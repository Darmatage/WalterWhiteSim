using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Start is called before the first frame update

    bool inRange = false;
    public bool carrying = false;
    public Transform hand;
    public GameObject itemInRange;
    public GameObject holding;
    public GameObject pickupHeld;
    public Transform place;

    public KeyCode pickupKey = KeyCode.Space;

    private bool pressed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(pickupKey) && inRange && !carrying && !pressed) {
            carrying = true;
            holding = itemInRange.transform.GetChild(0).gameObject;
            holding.SetActive(true);
            holding.transform.position = hand.position;
            holding.transform.parent = transform;
            itemInRange.SetActive(false);
            pickupHeld = itemInRange;
            pressed = true;
        }

        if (Input.GetKey(pickupKey) && carrying && !pressed) {
            carrying = false;
            pickupHeld.transform.position = place.position;

            holding.SetActive(false);
            holding.transform.position = pickupHeld.transform.position;
            holding.transform.parent = pickupHeld.transform;
            pickupHeld.SetActive(true);
            pressed = true;
        }

        if (Input.GetKeyUp(pickupKey)) {
            pressed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "item_pickup") {
            inRange = true;
            itemInRange = other.gameObject;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "item_pickup") {
            inRange = false;
        }
    }
}
