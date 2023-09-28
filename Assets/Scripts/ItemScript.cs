using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemScrObj itemObject;
    string itemName = "";
    private bool isClicked = false;
    public bool isPickedUp = false;

    private InventoryManager_II inventoryScript;

    public void Start()
    {
        itemName = itemObject.itemName;
        isPickedUp = false;
        inventoryScript = FindObjectOfType<InventoryManager_II>();
        
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
            inventoryScript.InventoryBool(isPickedUp);
            inventoryScript.AddToInventoryList(itemName);
            Destroy(gameObject);
        }
    }

    public string GiveItemName(string pickUpName)
    {
        pickUpName = itemName;
        return pickUpName;       
    }

}
