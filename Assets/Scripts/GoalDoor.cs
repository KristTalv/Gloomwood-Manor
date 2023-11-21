using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDoor : MonoBehaviour
{
    // String
    private string message;
    // Integer
    private int counter = 0;
    // Bools
    public bool isInRangeDoor = false;
    private bool isClicked = false;
    // ScriptableObjects
    [SerializeField] private QuestDialogScrObj dialogOption1;
    [SerializeField] private QuestDialogScrObj dialogOption2;
    [SerializeField] private QuestDialogScrObj dialogOption3;
    // Scripts
    private Puz_LightsOff puz_LightsOff;
    private DialogManager dialogManager;

    public void Start()
    {
        puz_LightsOff = FindObjectOfType<Puz_LightsOff>();
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
                if(hit.transform.name == gameObject.transform.name)
                {
                    isClicked = true;
                }
                if(hit.transform.name == gameObject.transform.name && isInRangeDoor == true)
                {
                    isClicked = true;
                    GiveDialogExit();
                }
                else
                {
                    isClicked = false;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        isInRangeDoor = true;
        puz_LightsOff.GiveRangeBool(isInRangeDoor);
        if (isClicked == true)
        {
            GiveDialogExit();  
        }
    }
    public void OnTriggerExit(Collider other)
    {
        isInRangeDoor = false;
        puz_LightsOff.GiveRangeBool(isInRangeDoor);
    }

    private void GiveDialogExit()
    {
        if (counter == 0)
        {
            message = dialogOption1.dialogText;
            dialogManager.Listener(message);
        }
        if (counter == 1)
        {
            message = dialogOption2.dialogText;
            dialogManager.Listener(message);
        }
        if (counter >= 2)
        {
            message = dialogOption3.dialogText;
            dialogManager.Listener(message);
        }
        counter++;
    }
}
