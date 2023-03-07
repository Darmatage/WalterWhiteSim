using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject item;
    public float delay = 2;
    public GameObject instance;
    bool pickedUp = false;
    bool prev = false;
    bool justPicked = false;
    void Start()
    {
        prev = item.transform.GetChild(0).GetComponent<ItemBounce>().bounce;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pickedUp);
        pickedUp = instance != null && instance.transform.parent != null && !justPicked;
        if (pickedUp) {
            StartCoroutine(Spawn());
            pickedUp = false;
            justPicked = true;
        }
        
    }

    IEnumerator Spawn() 
    {
        yield return new WaitForSeconds(2f);
        instance = (GameObject)Instantiate(item, transform.position, Quaternion.identity);
        justPicked = false;
    }
}
