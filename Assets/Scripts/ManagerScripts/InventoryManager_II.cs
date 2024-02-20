using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<string> pickedUpNameList = new List<string>(); // List of picked up items
    // SFX
    [SerializeField] private AudioSource audioPickUP;

    public void GiveStartItems()
    {
        for (int i = 0; i < begingItemScrObj.Length; i++)
        {
            pickedUpName = begingItemScrObj[i].itemName;
            pickedUpNameList.Add(pickedUpName);
        }
        StartCoroutine(BegingItemScrObj());
        itemCount = pickedUpNameList.Count;
    }
    public void ClearItems()
    {
        GameObject[] allChildren = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i++;
        }
        foreach (GameObject child in allChildren)
        {
            Destroy(child.gameObject);
        }
    }

    IEnumerator BegingItemScrObj()
    {
        for (int i = 0; i < pickedUpNameList.Count; i++)
        {
            pickedUpName = pickedUpNameList[i];
            Instantiate(itemSlot, this.transform);

            yield return new WaitForSeconds(0.01f);
        }
    }
    public void MakeUISlotsVisable()
    {
        for (int i = 0; i < pickedUpNameList.Count; i++)
        {
            GameObject slot = gameObject.transform.GetChild(i).gameObject;
            Debug.Log(slot.transform.name);
            Color tmp = slot.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            itemSlot.GetComponent<SpriteRenderer>().color = tmp;
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
        if (canPutInBool == true && !pickedUpNameList.Contains(pickedUpName)) // item script has tells that item has been picked up and the item name is not inthe list
                                                                           // --> add to list and make a slot for it the inventory
        {
            itemCount++;
            pickedUpNameList.Add(pickedUpName);
            MakeInvtSlot(pickUpName);
        }
    }

    private void MakeInvtSlot(string itemName) // Creates the inventory slot for the picked up item
    {
        audioPickUP.Play();
        Instantiate(itemSlot, this.transform);
        canPutInBool = false;
    }

    public void UseItem(string item) 
    {
        audioPickUP.Play();
        for (int i = 0; i < pickedUpNameList.Count; i++)
        {
            if (item == pickedUpNameList[i])
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
                pickedUpNameList.RemoveAt(i);
            }
        }
    }
}