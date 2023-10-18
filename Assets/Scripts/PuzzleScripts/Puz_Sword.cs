using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Sword : MonoBehaviour
{
    // strings
    public string statusSword = "";
    private string cursorSprite;
    // bools
    private bool isClicked = false;
    // ScribtableObjects
    [SerializeField] private ItemScrObj diaPuzzSword;
    // scripts
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;
    private InvSlot invSlot;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        statusSword = "Violet";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            invSlot = FindObjectOfType<InvSlot>();
            cursorSprite = invSlot.useItemName;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puzzle" && hit.transform.name == "Q_Sword & KnightGrave")
                {
                    isClicked = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isClicked == true)
        {
            statusSword = puzzleManager.status_Sword;
            string statusSigil = puzzleManager.status_Sigil;
            if (statusSword == "Violet")
            {
                puzzleManager.status_Sigil = "Yellow";
                puzzleManager.status_Sword = "Yellow";
                string dialogOptio = diaPuzzSword.itemText;
                dialogManager.Listener(dialogOptio);
            }
            Debug.Log("Miekka sanoo sigils status: " + puzzleManager.status_Sigil);
            if(statusSigil == "Green")
            {
                if (cursorSprite == "Icon_Cursor_Symbol")
                {
                    statusSword = "Green";
                    puzzleManager.status_Sword = statusSword;
                    Debug.Log(statusSword);

                    string message = "The sword got off the statue! I'll take it.";
                    dialogManager.Listener(message);

                    //Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isClicked = false;
    }
}
