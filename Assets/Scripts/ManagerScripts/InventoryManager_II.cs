using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager_II : MonoBehaviour
{
    //Strings
    public string pickedUpName;
    // Integers
    public int itemCount;
    // Bools
    private bool canPutInBool = false;
    //Scriptable Objects
    [SerializeField] private ItemScrObj[] begingItemScrObj;
    [SerializeField] private ItemScrObj[] itemScrObj; // All the item scribtaple objects
    // GameObjects
    [SerializeField] private GameObject itemSlot;
    [SerializeField] private GameObject dialogBox;
    // Lists
    List<string> pickedUpNames = new List<string>(); // List of picked up items
    // Scripts
    private InvSlot invSlot;
    // this code also uses:
    // - ItemScript
    // -Puz_Sword


    private void Start()
    {
        for (int i = 0; i < begingItemScrObj.Length; i ++)
        {
            pickedUpName = begingItemScrObj[i].itemName;
            pickedUpNames.Add(pickedUpName);
        }
        StartCoroutine(BegingItemScrObj());
        itemCount = pickedUpNames.Count;
    }

    IEnumerator BegingItemScrObj()
    {
        for (int i = 0; i < pickedUpNames.Count; i++)
        {
            pickedUpName = pickedUpNames[i];
            Instantiate(itemSlot, this.transform);
            yield return new WaitForSeconds(0.01f);
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
                Puz_Sword puz_Sword = hit.transform.GetComponent<Puz_Sword>();
                if (itemScript != null)
                {
                    pickedUpName = itemScript.GiveItemName(pickedUpName);
                    AddToInventoryList(pickedUpName);
                }
                if(puz_Sword != null)
                {
                    pickedUpName = puz_Sword.GiveSwordName(pickedUpName);
                    AddToInventoryList(pickedUpName);
                    //UseItem(item); 
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
            itemCount++;
            pickedUpNames.Add(pickedUpName);
            MakeInvtSlot(pickUpName);
        }
    }

    private void MakeInvtSlot(string itemName) // Creates the inventory slot for the picked up item
    {
        Instantiate(itemSlot, this.transform);
        canPutInBool = false;
    }

    public void UseItem(string item) 
    {
        for (int i = 0; i < pickedUpNames.Count; i++)
        {
            if (item == pickedUpNames[i])
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
                pickedUpNames.RemoveAt(i);
            }
        }
    }
}