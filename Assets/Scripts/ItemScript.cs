using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemScrObj itemObject;
    private bool isClicked = false;
    public bool isPickedUp = false;
    string itemName = "";

    public void Start()
    {
        itemName = itemObject.itemName;
        isPickedUp = false;
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //Chack if hitted object has itemscript --> is an item
                ItemScript itemScript = hit.transform.GetComponent<ItemScript>();
                if (itemScript != null)
                {
                    if (itemScript.itemName == itemName)
                    {
                        isClicked = true;
                    }
                }
                else
                {
                    isClicked = false;
                }
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player" && isClicked == true)
        {
            GiveItemName(itemName);
            isPickedUp = true;
            pickUpBool();
            Destroy(gameObject);
        }
    }
    public bool pickUpBool()
    {
        Debug.Log("ItemScript: " + isPickedUp);
        return isPickedUp;
    }
    public string GiveItemName(string pickUpName)
    {
        pickUpName = itemName;
        return pickUpName;       
    }

}
