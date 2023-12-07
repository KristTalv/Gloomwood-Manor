using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class Puz_LightsOff : MonoBehaviour
{
    // Strings
    public string statusLightsOff = "";
    private string message;
    private string cursorName;
    // Ints
    [SerializeField] private int timeLimit = 10;
    // Timer
    private Timer timer;
    // Bool
    private bool isClicked = false;
    private bool isExitDoorRange = false;
    private bool isStatueRange = false;
    private bool isTimeOut = false;
    private bool isGameWon = false;
    // Game Object
    [SerializeField] private GameObject sceneLight;
    [SerializeField] private GameObject startLight;
    [SerializeField] private GameObject sceneLightEnding;
    [SerializeField] private GameObject statueSwordObject;
    // Scriptable Objects
    [SerializeField] private QuestDialogScrObj dialog1;
    [SerializeField] private QuestDialogScrObj dialog2;
    // Scripts
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;
    private GoalDoor goalDoor;
    private MouseController mouseController;
    private InventoryManager_II inventoryManagerII;
    private Puz_LetLight puz_LetLight;
    private ScreenManager screenManager;

    void Start()
    {
        statusLightsOff = "Violet";

        dialogManager = FindObjectOfType<DialogManager>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        goalDoor = FindObjectOfType<GoalDoor>();
        mouseController = FindObjectOfType<MouseController>();
        inventoryManagerII = FindObjectOfType<InventoryManager_II>();
        puz_LetLight = FindObjectOfType<Puz_LetLight>();
        screenManager = FindObjectOfType<ScreenManager>();

        puzzleManager.statusLightsOff = statusLightsOff;
    }
    private void StartCounter()
    {
        timer = new Timer(1000);
        timer.Elapsed += CountDown;
        timer.Start();
    }
    private void CountDown(object sender, ElapsedEventArgs e)
    {
        timeLimit--;
        if(timeLimit <= 0)
        {
            isTimeOut = true;
            timer.Stop();
        }
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            { 
                if (hit.transform.name == "Door_Goal")
                {
                    cursorName = mouseController.mouseScriptCursorName;
                    isExitDoorRange = goalDoor.isInRangeDoor;
                    isClicked = true;
                }
                else if (hit.transform.name == "Q_LightsOff" && isStatueRange == true)
                {

                    isClicked = true;
                    message = dialog1.dialogText;
                    dialogManager.Listener(message);
                }
                else if (hit.transform.name == "Q_LightsOff")
                {
                    cursorName = mouseController.mouseScriptCursorName;
                    isClicked = true;
                }
                else
                {
                    isClicked = false;
                }

            }
        }
        if(isExitDoorRange == true && isClicked == true)
        {
            CheckTheDoor();
        }
        if(isTimeOut == true && isGameWon == false)
        {
            puzzleManager.statusLightsOff = "Red";
        }
    }

    private void CheckTheDoor()
    {
        string goodToGo = puzzleManager.statusSword;
        if(cursorName == "Icon_Cursor_SkeletonKey" && goodToGo == "Green" && statusLightsOff == "Yellow")
        {
            isGameWon = true;
            puzzleManager.statusLightsOff = "Green";     
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        isStatueRange = true;
        if (isClicked == true && isStatueRange)
        {
            message = dialog1.dialogText;
            dialogManager.Listener(message);
            if (cursorName == "Icon_Cursor_Sword")
            {
                statusLightsOff = "Yellow";
                puzzleManager.statusLightsOff = statusLightsOff;
                message = dialog2.dialogText;
                inventoryManagerII.UseItem("Sword");

                StartCounter();

                screenManager.TurnPitchAmbient();
                screenManager.StopBurningAudio();
                statueSwordObject.SetActive(true);
                puz_LetLight.LightSwitch(false);
                sceneLight.SetActive(false);
                startLight.SetActive(false);
                sceneLightEnding.SetActive(true);

                dialogManager.Listener(message);
            }
        }
        else if (isClicked == true)
        {
            Debug.Log("tataa");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isStatueRange = false;   
    }
    public bool GiveRangeBool(bool isInRange)
    {
        isExitDoorRange = isInRange;
        UpdateBool(isExitDoorRange);
        return isExitDoorRange;
    }
    private void UpdateBool(bool exitDoorRange)
    {
        bool exit = exitDoorRange;
    }
}
