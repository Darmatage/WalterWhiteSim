using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBounce : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject art;
    private GameObject shadow;
    public float ystep = 0.03f;
    public string shadowImgName = "testshadow_";
    private int currSprite;
    private string spriteName;
    private string removeName;
    public bool bounce = true;
    void Start()
    {
        art = transform.GetChild(0).gameObject;
        shadow = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (bounce) {
            shadow.SetActive(true);
            spriteName = shadow.GetComponent<SpriteRenderer>().sprite.name;
            // Debug.Log(spriteName);
            removeName = spriteName.Substring(shadowImgName.Length);
            // Debug.Log(removeName == "1");
            currSprite = int.Parse(removeName);
            art.transform.localPosition = new Vector3(0, ystep * currSprite, 0);
        } else {
            shadow.SetActive(false);
        }
    }
}
