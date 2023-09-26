using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int numOfSlots = 12;
    private ItemScript itemScript;
    private bool itemName = false;
    private string skeletonName = "";
    List<string> invenotryItems = new List<string>();
    //[SerializeField] private GameObject invSlot;

    void Start()
    {
        itemScript = FindObjectOfType<ItemScript>();
        //skeletonName = FindObjectOfType<ItemScript>().itemObject.itemName;

        //for (int i = 0; i < numOfSlots; i ++)
        //{
        //    Debug.Log("num: " + numOfSlots);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        itemName = itemScript.isPickedUp;
        if (Input.GetMouseButtonDown(0))
        {
            itemName = itemScript.isPickedUp;
            skeletonName = itemScript.GetComponent<ItemScript>().itemObject.itemName;
            Debug.Log("Inventory tiet‰‰: " + skeletonName);
            if(itemName == true)
            {
                invenotryItems.Add(skeletonName);


            }
            foreach (string name in invenotryItems)
            {
                Debug.Log("Listassa " + name);
            }
        }
        //Inventory ei toimi ollenkaan. Antaa v‰‰r‰‰ itemin nime‰ j lista ei muutenkaan v‰ltt‰m‰tt‰ aja asiaa. Mieti myˆhemmin. 
        // ItemScriptableObjekt on oko
        // ItemScriptin pit‰s palauttaa jotain t‰nne inventory manageriin
        
    }

}
