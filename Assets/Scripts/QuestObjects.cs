using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestObjects : MonoBehaviour
{

    // Strings
    private string clickedObjName;
    // Integers
    private int cauntStartDoor = 0;
    // GameObjects
    public GameObject dialogBox;
    // Bools
    private bool talkBool = false;
    private bool inRange = false;
    // ScriptableObjects
    [SerializeField] private QuestDialogScrObj doorStuck1;
    [SerializeField] private QuestDialogScrObj doorStuck2;
    [SerializeField] private QuestDialogScrObj doorStuck3;
    // Scripts
    private PuzzleManager puzzleManager;
    private DialogManager dialogManager;

    private void Start()
    {
        cauntStartDoor = 0;
        //dialogBox.SetActive(false); // Dioalog box is not visable
        puzzleManager = FindObjectOfType<PuzzleManager>(); // Puzzle manager is awailable now
        dialogManager = FindObjectOfType<DialogManager>();

    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //getting information of mouse position related to camera
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == gameObject.name) // if user clkicks a "quest" object, questBoolean is true
                {
                    clickedObjName = hit.transform.name;
                    talkBool = true;
                    if (inRange == true)
                    {
                        GiveDialog();
                    }
                }
                else
                {
                    talkBool = false;
                }               
            }
        }

    }
    private void OnTriggerEnter(Component other)
    {
        inRange = true;
        GiveDialog(); // Takes care of dialog box and dialog options
    }
    private void OnTriggerExit(Collider other) // exiting makes dialog box unvisable and sets questBoolean to false --> re-enttering trigger area wont make 
                                               // dialog box visable
    {
        dialogBox.SetActive(false);
        talkBool = false;
        inRange = false;
    }

    private void GiveDialog()
    {
        //if (dialogBox == true && talkBool == true)
        //{
        //    dialogBox.SetActive(false);
        //}

        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor == 0)
        {
            string message = doorStuck1.dialogText;
            dialogManager.Listener(message);
            //dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = doorStuck1.dialogText;
        }
        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor == 1)
        {
            string message = doorStuck2.dialogText;
            dialogManager.Listener(message);
        }
        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor >= 2)
        {
            string message = doorStuck3.dialogText;
            dialogManager.Listener(message);
        }
        if (talkBool == true)
        {
            dialogBox.SetActive(true);
            UpDateCaunt();
        }
        
    }

    private void UpDateCaunt()
    {
        cauntStartDoor++;
    }
}
