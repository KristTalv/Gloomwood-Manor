using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager_II : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private ItemScrObj[] itemScrObj; // All the item scribtaple objects
    [Header("Slot")]
    [SerializeField] private GameObject itemSlot;
    [Header("Dialog Box")]
    [SerializeField] private GameObject dialogBox;

    List<string> pickedUpNames = new List<string>(); // List of picked up items

    public string pickedUpName;
    private bool canPutInBool = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Check if hitted object has itemscript --> is an item
                ItemScript itemScript = hit.transform.GetComponent<ItemScript>();
                if (itemScript != null)
                {
                    pickedUpName = itemScript.GiveItemName(pickedUpName);
                    AddToInventoryList(pickedUpName);
                }
                else
                {
                    dialogBox.SetActive(false);
                }
            }           
        }        
    }

    public bool InventoryBool(bool canDo) // In item script is called to tell that the item has been collected --> true
    {
        canPutInBool = canDo;
        return canPutInBool;
    }
    public void AddToInventoryList(string pickUpName) 
    {
        if (canPutInBool == true && !pickedUpNames.Contains(pickedUpName)) // item script has tells that item has been picked up and the item name is not inthe list
                                                                           // --> add to list and make a slot for it the inventory
        {
            pickedUpNames.Add(pickedUpName);
            //foreach (string name in pickedUpNames)
            //{
            //    Debug.Log("Inventoryssä: " + name);
            //}
            MakeInvtSlot(pickUpName);
        }
       
    }
    private void MakeInvtSlot(string itemName) // Creates the inventory slot for the picked up item
    {
        //Debug.Log("Tee slotti: " + itemName);
        Instantiate(itemSlot, this.transform);
        canPutInBool = false;
    }
    public string Listener(string message) // This is called from inventory slot prefab --> inventory makes item dialog visible
    {
        if (dialogBox == true)
        {
            dialogBox.SetActive(false);
        }
        //Debug.Log("Item text: "+message);
        dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
        dialogBox.SetActive(true);
        return message;
    }

}
