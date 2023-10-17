using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Sword : MonoBehaviour
{
    // strings
    public string statusSword = "";
    // bools
    private bool isClicked = false;
    // ScribtableObjects
    [SerializeField] private ItemScrObj diaPuzzSword;
    // scripts
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;
    //private Puz_Sigil puz_Sigil;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        //puz_Sigil = FindObjectOfType<Puz_Sigil>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        statusSword = "Violet";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puzzle" && hit.transform.name == "Q_Sword & KnightGrave")
                {
                    Debug.Log(hit.transform.name);
                    isClicked = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isClicked == true)
        {
            puzzleManager.puz_Sigil_Status = "Yellow";
            Debug.Log("Sword: " + puzzleManager.puz_Sigil_Status);
            statusSword = "Yellow"; 
            string dialogOptio = diaPuzzSword.itemText;
            dialogManager.Listener(dialogOptio);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isClicked = false;
    }
}
