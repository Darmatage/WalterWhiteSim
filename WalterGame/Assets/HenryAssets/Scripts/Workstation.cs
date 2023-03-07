using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workstation : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> items = new List<GameObject>();
    public GameObject gameHandler;
    public int maxItemCapacity = 5;
    public Transform[] itemSlots;
    private GameHandler handler;
    public bool isDistill;
    
    void Start()
    {
        handler = gameHandler.GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool addItem(GameObject item) {
        if (items.Count >= maxItemCapacity) {
            return false;
        } else {
            items.Add(item);
            updateDisplay();
        }
        return true;
    }

    public void take(GameObject elem) {
        items.Remove(elem);
    }

    private void updateDisplay() {
        for (int i = 0; i < items.Count; i++) {
            if (items[i] != null) {
                items[i].SetActive(true);
                items[i].transform.position = itemSlots[i].position;
                items[i].transform.parent = itemSlots[i];
            }
        }
    }

    public void craft() {
        
        (GameObject, int) check;
        if (isDistill) {
            check = handler.getDistillRecipe(items);
        } else {
            check = handler.getRecipe(items);
        }
        
        if (check.Item2 > -1) {
            while (items.Count > 0) {
                Destroy(items[0]);
                items.RemoveAt(0);
            }
            
            GameObject crafted = (GameObject)Instantiate(check.Item1, itemSlots[0].position, Quaternion.identity);
            Debug.Log(crafted.name);
            if (handler.getID(crafted.tag) == handler.getID("item_meth")) { //if its meth
                handler.addMeth(check.Item2);
                Destroy(crafted);
            } else {
                crafted.transform.parent = itemSlots[0];
                items.Add(crafted);
            }
        } 
    }
}
