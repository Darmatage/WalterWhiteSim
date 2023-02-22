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
    private IDictionary<string, (GameObject, int)> recipes = new Dictionary<string, (GameObject, int)>();
    public GameObject empty;
    public int meth = 0;
    public GameObject methText;
    void Start()
    {
        updateText();
        empty = new GameObject();
        for (int i = 0; i < ingredients.Length; i++) {
            Debug.Log(ingredients[i].tag);
            ingredientIDs.Add(ingredients[i].tag, i);
        }
        string recipe = "0-3";
        recipes.Add("0-2", (ingredients[2], 1)); // Two pills = one powder
        recipes.Add("0-1,2-1", (ingredients[1], 1)); //pill + powder = 1 meth
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

    public string getRecipe(List<GameObject> items) {
        IDictionary<int, int> counts = new Dictionary<int, int>();
        for (int i = 0; i < items.Count; i++) {
            int currID = this.getID(items[i].tag);
            if (counts.ContainsKey(currID)) {
                counts[currID] += 1;
            } else {
                counts.Add(currID, 1);
            }
        }
        string recipe = "";
        for (int i = 0; i < ingredients.Length; i++) {
            if (counts.ContainsKey(i)) {
                recipe += i + "-" + counts[i] + ",";
            }
        }
        recipe = recipe.Substring(0, recipe.Length - 1);
        return recipe;
    }

    public void addMeth(int amount) {
        meth += amount;
        updateText();
    }

    private void updateText() {
        methText.GetComponent<Text>().text = "" + meth;
    }
}
