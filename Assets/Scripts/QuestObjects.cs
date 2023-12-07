using System.Collections.Generic;
using UnityEngine;


public class QuestObjects : MonoBehaviour
{
    // Arrays
    private float[] yCordinateArray = { 0f, 0f, 0f }; 
    // Strings
    private string clickedObjectName;
    // Integers
    private int cauntStartDoor = 0;
    int graveIndex = 0; // For caunting the closest grave
    // GameObjects
    public GameObject dialogBox;
    // Bools
    private bool isTalking = false;
    private bool isInRange = false;
    // ScriptableObjects
    [Header("Start Door")]
    [SerializeField] private QuestDialogScrObj[] doorDialogOption;
    // RightSideGraves
    [Header("Right Side Graves")]
    [SerializeField] private QuestDialogScrObj graveDialogEmma;
    [SerializeField] private QuestDialogScrObj graveDialogRichard;
    [SerializeField] private QuestDialogScrObj graveDialogDusty;
    // LeftSideGraves
    [Header("Left Side Graves")]
    [SerializeField] private QuestDialogScrObj graveDialogWilamm;
    [SerializeField] private QuestDialogScrObj graveDialogSirEdward;
    [SerializeField] private QuestDialogScrObj graveDialogRamsey;
    // Lists
    List<QuestDialogScrObj> graveRightDialogOptionList = new List<QuestDialogScrObj>();
    List<QuestDialogScrObj> graveLeftDialogOptionList = new List<QuestDialogScrObj>(); 
    // Vector 3
    private Vector3 clickPosVector3;
    // Scripts
    private PuzzleManager puzzleManager;
    private DialogManager dialogManager;
    private Puz_Sigil puz_Sigil;
    private Config config;

    private void Start()
    {
        cauntStartDoor = 0;
        puzzleManager = FindObjectOfType<PuzzleManager>(); // Puzzle manager is awailable now
        dialogManager = FindObjectOfType<DialogManager>();
        puz_Sigil = FindObjectOfType<Puz_Sigil>();
        config = FindObjectOfType<Config>();

        for (int i = 0; i < yCordinateArray.Length; i++)
        {
            yCordinateArray[i] = config.yCordinateArray[i];
        }

        graveRightDialogOptionList.Add(graveDialogDusty);
        graveRightDialogOptionList.Add(graveDialogRichard);
        graveRightDialogOptionList.Add(graveDialogEmma);

        graveLeftDialogOptionList.Add(graveDialogWilamm);
        graveLeftDialogOptionList.Add(graveDialogSirEdward);
        graveLeftDialogOptionList.Add(graveDialogRamsey);
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
                    clickedObjectName = hit.transform.name;
                    clickPosVector3 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    isTalking = true;
                    if (isInRange == true)
                    {
                        GiveDialog();
                    }
                }
                else
                {
                    isTalking = false;
                }               
            }
        }
    }
    private void OnTriggerEnter(Component other)
    {
        isInRange = true;
        if (isTalking == true)
        {
            GiveDialog(); // Takes care of dialog box and dialog options
        }
    }
    private void OnTriggerExit(Collider other) // exiting makes dialog box unvisable and sets questBoolean to false --> re-enttering trigger area wont make 
                                               // dialog box visable
    {
        dialogBox.SetActive(false);
        isTalking = false;
        isInRange = false;
        //dialogManager.ResetListener();
    }

    private void GiveDialog()
    {
        string message;
        if (clickedObjectName == "Door_Start")
        {
            for (int i = 0; i < doorDialogOption.Length; i++)
            {
                if (i == cauntStartDoor)
                {
                    message = doorDialogOption[i].dialogText;
                    dialogManager.Listener(message);

                }
                else if (cauntStartDoor >= 2)
                {
                    message = doorDialogOption[2].dialogText;
                    dialogManager.Listener(message);
                }
            }
        }

        // Calculate correct grave
        if (clickedObjectName == "RightSideGraves" && isTalking == true && isInRange == true)
        {
            float smallestResult = float.MaxValue;
            for (int i = 0; i < yCordinateArray.Length; i++)
            {
                float resutl = yCordinateArray[i] - clickPosVector3.y;
                if (Mathf.Abs(resutl) < smallestResult)
                {
                    smallestResult = resutl;
                    graveIndex = i;
                }
            }
            message = graveRightDialogOptionList[graveIndex].dialogText;
            dialogManager.Listener(message);
        }
        if (clickedObjectName == "LeftSideGraves" && isTalking == true && isInRange == true)
        {
            float smallestResult = float.MaxValue;
            for (int i = 0; i < yCordinateArray.Length; i++)
            {
                float resutl = yCordinateArray[i] - clickPosVector3.y;
                if (Mathf.Abs(resutl) < smallestResult)
                {
                    smallestResult = resutl;
                    graveIndex = i;
                }
            }
            if (graveIndex == 1 )
            {
                string status = puzzleManager.statusSigil;
                if(status == "Yellow")
                {
                    puz_Sigil.PickUpSigil();
                }
            }
            message = graveLeftDialogOptionList[graveIndex].dialogText;
            dialogManager.Listener(message);
        }
        if (isTalking == true)
        {
            dialogBox.SetActive(true);
        }
        UpDateCaunt();
    }

    private void UpDateCaunt()
    {
        cauntStartDoor++;
    }
}
