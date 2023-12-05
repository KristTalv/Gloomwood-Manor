using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_LetLight : MonoBehaviour
{
    // Strings
    public string statusLight = "";
    public string diaPuzzLetLight;
    private string cursorSprite = "";
    // Bools
    private bool isClicked = false;
    private bool isRange = false;
    //GameObjects
    [SerializeField] private GameObject sceneLights;
    [SerializeField] private GameObject[] flameParticleArray;   
    // Scriptable Objects
    [SerializeField] public QuestDialogScrObj dialogLetLight1;
    [SerializeField] public QuestDialogScrObj dialogLetLight2;
    // Scripts
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;
    private MouseController mouseController;


    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        mouseController = FindObjectOfType<MouseController>();
        statusLight = "Violet";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Q_LetLight")
                {
                    isClicked = true;
                    cursorSprite = mouseController.mouseScriptCursorName;
                    if(cursorSprite == "Icon_Cursor_Lighter" && isClicked == true && isRange == true)
                    {
                        DoLightPuzzle();
                    }
                    if (isRange == true)
                    {
                        GiveDialog();
                    }
                }
                else
                {
                    cursorSprite = "";
                    isClicked = false;
                }
            }
        }     
    }

    private void GiveDialog()
    {
        if (statusLight == "Violet")
        {
            diaPuzzLetLight = dialogLetLight1.dialogText;
        }
        else if (statusLight == "Green")
        {
            diaPuzzLetLight = dialogLetLight2.dialogText;
        }
        dialogManager.Listener(diaPuzzLetLight);
    }

    private void DoLightPuzzle()
    {
        statusLight = "Green";
        puzzleManager.statusLetLight = statusLight;
        diaPuzzLetLight = dialogLetLight2.dialogText;
        dialogManager.Listener(diaPuzzLetLight);
        sceneLights.SetActive(true);
        LightSwitch(true);
        Destroy(gameObject);
    }
    public bool LightSwitch(bool isOn)
    {
        if (isOn == true)
        {
            for (int i = 0; i < flameParticleArray.Length; i++)
            {
                flameParticleArray[i].SetActive(true);
            }
        }
        else if (!isOn)
        {
            for (int i = 0; i < flameParticleArray.Length; i++)
            {
                flameParticleArray[i].SetActive(false);
            }
        }
        return isOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        isRange = true;
        if (isClicked == true && cursorSprite == "Icon_Cursor_Lighter")
        {
            DoLightPuzzle();
        }
        else if (isClicked)
        {
            GiveDialog();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isRange = false;
    }
}
