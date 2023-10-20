using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class Puz_LightsOff : MonoBehaviour
{
    // Strings
    public string status_LightsOff = "";
    private string message;
    private string cursorName;
    // Ints
    [SerializeField] private int timeLimit = 10;
    // Timer
    private Timer ajastin;
    // Bool
    private bool isClicked = false;
    private bool exitDoorRange = false;
    private bool timeOut = false;
    // Game Object
    [SerializeField] private GameObject vfxParticle;
    [SerializeField] private GameObject sceneLight;
    [SerializeField] private GameObject startLight;
    [SerializeField] private GameObject sceneLightEnding;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject gameOverScreen;
    // Scriptable Objects
    [SerializeField] private QuestDialogScrObj dialog1;
    [SerializeField] private QuestDialogScrObj dialog2;
    // Scripts
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;
    private GoalDoor goalDoor;
    private MouseController mouseController;

    void Start()
    {
        status_LightsOff = "Violet";

        dialogManager = FindObjectOfType<DialogManager>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        goalDoor = FindObjectOfType<GoalDoor>();
        mouseController = FindObjectOfType<MouseController>();

        puzzleManager.status_LightsOff = status_LightsOff;


    }
    private void StartCounter()
    {
        ajastin = new Timer(1000);
        ajastin.Elapsed += CountDown;
        ajastin.Start();
    }
    private void CountDown(object sender, ElapsedEventArgs e)
    {
        timeLimit--;
        if(timeLimit <= 0)
        {
            timeOut = true;
            Debug.Log("Aika loppu! " + timeOut);
            //gameOverScreen.SetActive(true);
            ajastin.Stop();
        }
        else
        {
            Debug.Log(timeLimit);
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
                if (hit.transform.name == "Q_LightsOff")
                {
                    cursorName = mouseController.mouseScriptCursorName;
                    isClicked = true;
                }
                if (hit.transform.name == "Door_Goal")
                {
                    exitDoorRange = goalDoor.inRangeExitDoor;
                    isClicked = true;
                }
            }
        }
        if(exitDoorRange == true && isClicked == true)
        {
            cursorName = mouseController.mouseScriptCursorName;
            CheckTheDoor();
        }
        if(timeOut == true)
        {
            gameOverScreen.SetActive(true);
            timeOut = false;
        }
    }

    private void CheckTheDoor()
    {
        string goodToGo = puzzleManager.status_Sword;
        if(cursorName == "Icon_Cursor_SkeletonKey" && goodToGo == "Green")
        {
            endScreen.SetActive(true);
            // Kirjoita koodi joka tsekkaa ett� aijemmat puzzlet --> done
            // JA jos aika ei loppu s�� voitat
            // eli tee viel� laskuri
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isClicked == true )
        {

            message = dialog1.dialogText;
            dialogManager.Listener(message);
            if (cursorName == "Icon_Cursor_Sword")
            {
                status_LightsOff = "Yellow";
                puzzleManager.status_LightsOff = status_LightsOff;
                message = dialog2.dialogText;

                StartCounter();

                vfxParticle.SetActive(false);
                sceneLight.SetActive(false);
                startLight.SetActive(false);
                sceneLightEnding.SetActive(true);

                dialogManager.Listener(message);
            }
        }
    }
    public bool GiveRangeBool(bool isInRange)
    {
        exitDoorRange = isInRange;
        UpdateBool(exitDoorRange);
        Debug.Log(exitDoorRange);
        return exitDoorRange;
    }
    private void UpdateBool(bool exitDoorRange)
    {
        bool exit = exitDoorRange;
    }

}