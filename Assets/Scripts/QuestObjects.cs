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
    // Floats
    [SerializeField] private float y1_0 = 0.7f; 
    [SerializeField] private float y1_1 = 0.6f; 
    [SerializeField] private float y1_2 = 0.5f; 
    int graveIndex = 0; // For caunting the closest grave
    // GameObjects
    public GameObject dialogBox;
    [SerializeField] private GameObject[] graveObject;
    // Bools
    private bool talkBool = false;
    private bool inRange = false;
    // ScriptableObjects
    [SerializeField] private QuestDialogScrObj doorStuck1;
    [SerializeField] private QuestDialogScrObj doorStuck2;
    [SerializeField] private QuestDialogScrObj doorStuck3;
    // RightSideGraves
    [SerializeField] private QuestDialogScrObj graveEmma1;
    [SerializeField] private QuestDialogScrObj graveRichard1;
    [SerializeField] private QuestDialogScrObj graveDusty;
    // LeftSideGraves
    //[SerializeField] private QuestDialogScrObj graveWilamm;
    //[SerializeField] private QuestDialogScrObj graveSirEdward;
    // Lists
    List<float> y1 = new List<float>();
    List<QuestDialogScrObj> graveDialogOptio = new List<QuestDialogScrObj>();
    // Vector 3
    private Vector3 clickPos;
    // Scripts
    private PuzzleManager puzzleManager;
    private DialogManager dialogManager;

    private void Start()
    {
        cauntStartDoor = 0;
        puzzleManager = FindObjectOfType<PuzzleManager>(); // Puzzle manager is awailable now
        dialogManager = FindObjectOfType<DialogManager>();

        y1.Add(y1_0);
        y1.Add(y1_1);
        y1.Add(y1_2);

        graveDialogOptio.Add(graveDusty);
        graveDialogOptio.Add(graveRichard1);
        graveDialogOptio.Add(graveEmma1);

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
                    //clickPos = Input.mousePosition;
                    clickPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
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

        string message;
        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor == 0)
        {
            message = doorStuck1.dialogText;
            dialogManager.Listener(message);
        }
        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor == 1)
        {
            message = doorStuck2.dialogText;
            dialogManager.Listener(message);
        }
        if (clickedObjName == "Door_Start" && talkBool == true && cauntStartDoor >= 2)
        {
            message = doorStuck3.dialogText;
            dialogManager.Listener(message);
        }
        if (clickedObjName == "RightSideGraves" && talkBool == true)
        {
            float smallestResult = 100000000;
            for (int i = 0; i < y1.Count; i++)
            {
                float resutl = y1[i] - clickPos.y;
                if (Mathf.Abs(resutl) < smallestResult)
                {
                    smallestResult = resutl;
                    graveIndex = i;
                }
            }
            message = graveDialogOptio[graveIndex].dialogText;
            dialogManager.Listener(message);
        }
        //if (clickedObjName == "Coffing_Emma" && talkBool == true)
        //{
        //    message = graveEmma1.dialogText;
        //    dialogManager.Listener(message);
        //}
        //if (clickedObjName == "Richard_Grave" && talkBool == true)
        //{
        //    message = graveRichard1.dialogText;
        //    dialogManager.Listener(message);
        //}
        //if (clickedObjName == "Grave_Dusty" && talkBool == true)
        //{
        //    message = graveDusty.dialogText;
        //    dialogManager.Listener(message);
        //}
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
