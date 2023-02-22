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

    public GameObject station;
    private bool stationInRange = false;

    public KeyCode pickupKey = KeyCode.Space;

    private bool pressed = false;
    private bool added = false;

    public KeyCode craftKey = KeyCode.C;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(pickupKey) && inRange && !carrying && !pressed) {
            carrying = true;
            
            itemInRange.transform.position = hand.position;
            itemInRange.transform.parent = transform;
            itemInRange.transform.GetChild(0).GetComponent<ItemBounce>().bounce = false;
            pickupHeld = itemInRange;
            pressed = true;
        }

        if (Input.GetKey(pickupKey) && carrying && !pressed && stationInRange) {
            added = station.GetComponent<Workstation>().addItem(pickupHeld);
            if (added) {
                carrying = false;
                pickupHeld.transform.GetChild(0).GetComponent<ItemBounce>().bounce = true;
            }
            pressed = true;
        }

        if (Input.GetKeyUp(pickupKey)) {
            pressed = false;
        }

        if (stationInRange && Input.GetKeyUp(craftKey)) {
            
            station.GetComponent<Workstation>().craft();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Substring(0, 4) == "item") {
            inRange = true;
            itemInRange = other.gameObject;
            
        }
        if (other.gameObject.tag == "takes_ingredient") {
            SetStationInRange(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.tag.Substring(0, 4) == "item") {
            inRange = false;
        }
        if (other.gameObject.tag == "takes_ingredient") {
            SetStationOutRange();
        }
    }

    public void SetStationInRange(GameObject work) {
        stationInRange = true;
        station = work;
    }

    public void SetStationOutRange() {
        stationInRange = false;
    }
}
