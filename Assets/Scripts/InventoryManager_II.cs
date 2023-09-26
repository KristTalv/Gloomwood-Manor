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
    List<GameObject> itemObjects = new List<GameObject>(); // List of all the item names

    private string pickedUpName;
    private bool canPutInBool = false;

    private ItemScript itemScript;

    void Start()
    {

        for (int i = 0; i < itemScrObj.Length; i++)
        {
            invenotryItemNames.Add(itemScrObj[i].itemName);
            //Debug.Log(itemScrObj[i].itemName);
        }
        foreach(string name in invenotryItemNames)
        {
            itemObjects.Add(FindObjectOfType<GameObject>()); // En ole vielä varma tarvitsenko tätä listaa.
        }

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Instantiate(itemSlot, this.transform);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Check if hitted object has itemscript --> is an item
                ItemScript itemScript = hit.transform.GetComponent<ItemScript>();
                if (itemScript != null)
                {

                    //itemScript = FindObjectOfType<ItemScript>();
                    canPutInBool = itemScript.pickUpBool();
                    pickedUpName = itemScript.GiveItemName(pickedUpName);


                    //pickedUpNames.Add(pickedUpName);
                    //foreach (string name in pickedUpNames)
                    //{
                    //    Debug.Log(name + " on pickUpNames listassa.");
                    //}
                }
            }           
            if (canPutInBool == true && !pickedUpNames.Contains(pickedUpName))
            {
                Debug.Log("Invenotryn poimitut esineet: " + pickedUpName + " Bool: " + canPutInBool);
                
                pickedUpNames.Add(pickedUpName);
                foreach(string name in pickedUpNames)
                {
                    Debug.Log("Inventoryssä: " + name);
                }
            }
        }        
    }

    private void MakeInvtSlot(string itemName)
    {

    }


}
