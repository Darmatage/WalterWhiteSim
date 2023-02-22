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
    
    void Start()
    {
        
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

    private void updateDisplay() {
        for (int i = 0; i < items.Count; i++) {
            items[i].SetActive(true);
            items[i].transform.position = itemSlots[i].position;
            items[i].transform.parent = itemSlots[i];
        }
    }

    public void craft() {
        string recipe = gameHandler.GetComponent<GameHandler>().getRecipe(items);
        Debug.Log(recipe);
        (GameObject, int) check = gameHandler.GetComponent<GameHandler>().checkForRecipe(recipe);
        if (check.Item2 > -1) {
            while (items.Count > 0) {
                Destroy(items[0]);
                items.RemoveAt(0);
            }
            
            GameObject crafted = (GameObject)Instantiate(check.Item1, itemSlots[0].position, Quaternion.identity);
            crafted.transform.parent = itemSlots[0];
            items.Add(crafted);
        } 
    }
}
