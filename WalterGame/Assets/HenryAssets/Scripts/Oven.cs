using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oven : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject input;
    public float timer;
    private bool hasItem = false;
    public bool cooking = false;
    private int minGoodTime, maxGoodTime;
    private int cookAmount;
    public Transform itemSlot;
    private GameObject wellCooked;
    private GameObject poorlyCooked;
    public GameObject gameHandler;
    private GameHandler handler;
    public GameObject timeText;
    void Start()
    {
        handler = gameHandler.GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooking) {
            timer += Time.deltaTime;
            Debug.Log(timer);
            timeText.GetComponent<Text>().text = "" + timer;
        }

    }

    public bool addItem(GameObject item) {
        if (hasItem || cooking) {
            return false;
        } else {
            
            item.SetActive(true);
            item.transform.position = itemSlot.position;
            item.transform.parent = itemSlot;
            input = item;
            hasItem = true;
        }
        return true;
    }

    public void take(GameObject elem) {
        hasItem = false;
    }

    public void craft() {
        if (hasItem) {
            (GameObject, GameObject, int, int, int) recipe;
                recipe = handler.getOvenRecipe(input);
            if (recipe.Item3 > -1) {
                cooking = true;
                Destroy(input);
                hasItem = false;
                wellCooked = recipe.Item1;
                poorlyCooked = recipe.Item2;
                cookAmount = recipe.Item3;
                minGoodTime = recipe.Item4 - recipe.Item5;
                maxGoodTime = recipe.Item4 + recipe.Item5;
                

            } 
        } else if(cooking) {
            cooking = false;
            GameObject cooked;
            
            if (timer >= minGoodTime && timer <= maxGoodTime) {
                cooked = wellCooked;
            } else {
                cooked = poorlyCooked;
            }
            GameObject crafted = (GameObject)Instantiate(cooked, itemSlot.position, Quaternion.identity);
            if (handler.getID(crafted.tag) == handler.getID("item_meth")) { //if its meth
                handler.addMeth(cookAmount);
                Destroy(crafted);
            } else {
                hasItem = true;
                crafted.transform.parent = itemSlot;
                input = crafted;
            }
            timer = 0;
        }
    }

    

    
}
