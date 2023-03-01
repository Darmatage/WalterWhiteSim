using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Convert a gameobject tag to its index in ingredients array, for fast lookup
    private IDictionary<string, int> ingredientIDs = new Dictionary<string, int>();
    
    public GameObject[] ingredients;
    // Convert a key representing ingredients and amounts to a tuple of a GameObject and a yield
    // recipe is a string with five ids ex -1|1|-1|-1|-1 means input is item id #1 in slot 2, the rest empty 
    private IDictionary<string, (GameObject, int)> recipes = new Dictionary<string, (GameObject, int)>();
    public GameObject empty;
    public int meth = 0;
    public GameObject methText;
    private List<List<int>> masks = new List<List<int>>();
    public int workstationSlots = 5;
    void Start()
    {
        updateText();
        for (int i = 0; i < workstationSlots - 1; i++) {
            List<int> m = new List<int>();
            for (int j = 0; j < workstationSlots; j++) {
                if (j <= i) {
                    m.Add(1);
                } else {
                    m.Add(0);
                }
            }
            masks = concatList(masks, permute(m), false);
        }
        for (int i = 0; i < masks.Count; i++) {
            List<int> curr = masks[i];
            string str = "";
            for (int j = 0; j < curr.Count; j++) {
                str += curr[j] + ", ";
            }
            Debug.Log(str);
        }

        empty = new GameObject();
        for (int i = 0; i < ingredients.Length; i++) {
            // Debug.Log("TAG:" + ingredients[i].name + ", " + ingredients[i].tag);
            ingredientIDs.Add(ingredients[i].tag, i);
        }
        ingredientIDs.Add("", -1); //empty tag gets -1

        Debug.Log(getID("item_pill"));

        generateRecipe(new string[]{"item_pill", "item_pill", "", "", ""}, "item_powder", 1);
        generateRecipe(new string[]{"item_pill", "item_powder", "", "", ""}, "item_meth", 1);
        
    }

    private bool ListContains(List<List<int>> list, List<int> has) {
        for (int i = 0; i < list.Count; i++) {
            List<int> curr = list[i];
            if (curr.Count == has.Count) {
                bool eq = true;
                for (int j = 0; j < curr.Count; j++) {
                    if (curr[j] != has[j]) {
                        eq = false;
                        break;
                    }
                }
                if (eq) {
                    return true;
                }
            }
        }
        return false;
    }

    private void generateRecipe(string[] items, string output, int amount) {
        List<int> recipe = new List<int>();
        for (int i = 0; i < items.Length; i++) {
            recipe.Add(getID(items[i]));
        }
        List<string> rs = makeShapeless(recipe);
        foreach (var r in rs) {
            if (!recipes.ContainsKey(r)) {
                recipes.Add(r, (ingredients[getID(output)], amount));
            }
        }

    }

    private List<string> makeShapeless(List<int> ids) {
        List<string> res = new List<string>();
        List<List<int>> permutations = permute(ids);
        for (int i = 0; i < permutations.Count; i++) {
            string recipe = "";
            List<int> curr = permutations[i];
            for (int j = 0; j < curr.Count; j++) {
                recipe += curr[j] + "";
                if (j < curr.Count - 1) {
                    recipe += "|";
                }
            }
            res.Add(recipe);
        }
        return res;
    }

    private List<List<int>> permute(List<int> list) {
        List<List<int>> res = new List<List<int>>();
        if (list.Count == 1) {
            res.Add(list);
            return res;
        }
        for (int i = 0; i < list.Count; i++) {
            List<int> curr = new List<int>(list);
            curr.RemoveAt(i);
            List<List<int>> perms = permute(curr);
            for (int j = 0; j < perms.Count; j++) {
                perms[j].Insert(0, list[i]);
            }
            res = concatList(res, perms, false);
        }
        return res;
    }

    List<List<int>> concatList(List<List<int>> list1, List<List<int>> list2, bool allowDuplicates) {
        List<List<int>> res = new List<List<int>>();
        foreach (var item in list1) {
            if (allowDuplicates) {
                res.Add(item);
            } else if (!ListContains(res, item)) {
                res.Add(item);
            }
        }
        foreach (var item in list2) {
            if (allowDuplicates) {
                res.Add(item);
            } else if (!ListContains(res, item)) {
                res.Add(item);
            }
        }
        return res;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public (GameObject, int) checkForRecipe(string key) {
        (GameObject, int) result = (empty, -1);
        if (recipes.ContainsKey(key)) {
            result = recipes[key];
        }

        return result;

    }

    public int getID(string objTag) {
        int result = -1;
        if (ingredientIDs.ContainsKey(objTag)) {
            result = ingredientIDs[objTag];
        } else {
            Debug.Log("unknown tag:" + objTag);
        }
        return result;
    }

    public (GameObject, int) getRecipe(List<GameObject> items) {
        
        string recipe = "";
        for (int i = 0; i < workstationSlots; i++) {
            if (i < items.Count) {
                recipe += getID(items[i].tag) + "|";
            } else {
                recipe += "-1|";
            }
        }

        recipe = recipe.Substring(0, recipe.Length - 1);
        Debug.Log(recipe);
        return checkForRecipe(recipe);
    }

    public void addMeth(int amount) {
        meth += amount;
        updateText();
    }

    private void updateText() {
        methText.GetComponent<Text>().text = "" + meth;
    }
}
