using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestObjects : MonoBehaviour
{
    // Dialog UI element and boolean to turn it on or of
    public GameObject dialogBox;
    private bool talkBool = false;

    // dialog options
    [SerializeField] private QuestDialogScrObj doorStuck1;
    [SerializeField] private QuestDialogScrObj doorStuck2;
    [SerializeField] private QuestDialogScrObj doorStuck3;

    //scripts
    private PuzzleManager puzzleManager;

    private string clickedObjName;

    private int cauntStartDoor = 0;


    private void Start()
    {
        cauntStartDoor = 0;
        dialogBox.SetActive(false); // Dioalog box is not visable
        puzzleManager = FindObjectOfType<PuzzleManager>(); // Puzzle manager is awailable now

    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //getting information of mouse position related to camera
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "quest") // if user clkicks a "quest" object, questBoolean is true
                {
                    clickedObjName = hit.transform.name;
                    talkBool = true;

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
        GiveDialog(); // Takes care of dialog box and dialog options
    }
    private void OnTriggerExit(Collider other) // exiting makes dialog box unvisable and sets questBoolean to false --> re-enttering trigger area wont make 
                                               // dialog box visable
    {
        dialogBox.SetActive(false);
        talkBool = false;
    }

    private void GiveDialog()
    {
        if (dialogBox == true && talkBool == true)
        {
            dialogBox.SetActive(false);
        }

        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor == 0)
        {
            dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = doorStuck1.dialogText;
        }
        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor == 1)
        {
            dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = doorStuck2.dialogText;
        }
        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor >= 2)
        {
            dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = doorStuck3.dialogText;
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
