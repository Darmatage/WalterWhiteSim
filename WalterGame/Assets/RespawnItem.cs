using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    public GameObject itemPrefab; // The item prefab to be spawned
    public float respawnTime = 2.0f; // The time in seconds to wait before respawning the item

    private GameObject currentItem; // The current instance of the item in the scene

    private void Start()
    {
        // Spawn the item at this script's position and rotation
        SpawnItem();
    }

    private void SpawnItem()
    {
        // Spawn the item at this script's position and rotation
        currentItem = Instantiate(itemPrefab, transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        // If the current item has been disabled, respawn it immediately
        if (currentItem != null)
        {
            SpawnItem();
        }
    }
}
