using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager_II : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private ItemScrObj[] itemScrObj; // All the item scribtaple objects
    [Header("Slot")]
    [SerializeField] private GameObject itemSlot;

    List<string> invenotryItemNames = new List<string>(); // List of all the item names
    List<string> pickedUpNames = new List<string>(); // List of picked up items

    public string pickedUpName;
    private bool canPutInBool = false;

    //private ItemScript itemScript;

    void Start()
    {

        for (int i = 0; i < itemScrObj.Length; i++)
        {
            invenotryItemNames.Add(itemScrObj[i].itemName);
            //Debug.Log(itemScrObj[i].itemName);
        }
    }

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
            }           
        }        
    }


    public bool InventoryBool(bool canDo)
    {
        canPutInBool = canDo;
        return canPutInBool;
    }
    public void AddToInventoryList(string pickUpName)
    {
        if (canPutInBool == true && !pickedUpNames.Contains(pickedUpName))
        {
            pickedUpNames.Add(pickedUpName);
            foreach (string name in pickedUpNames)
            {
                Debug.Log("Inventoryssä: " + name);
            }
            MakeInvtSlot(pickUpName);
        }
       
    }
    private void MakeInvtSlot(string itemName)
    {
        Debug.Log("Tee slotti: " + itemName);
        Instantiate(itemSlot, this.transform);
        canPutInBool = false;

    }


}
