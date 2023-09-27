using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestObjects : MonoBehaviour
{
    // Dialog UI element and boolean to turn it on or of
    public GameObject dialogBox;
    private bool talkBool = false;

    // dialog options
    public QuestDialogScrObj dialogA;
    public QuestDialogScrObj dialogB;

    //scripts
    private PuzzleManager puzzleManager;


    private void Start()
    {
        dialogBox.SetActive(false); // Dioalog box is not visable
        dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogA.dialogText;

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
                                                             // --> OnTrigger if() condition is true --> Dialog box is visable for user
                {
                    talkBool = true;
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
        if (dialogBox == true)
        {
            dialogBox.SetActive(false);
        }
        if (talkBool == true)
        {
            dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogA.dialogText;
            dialogBox.SetActive(true);
        }
        if (puzzleManager.puz_button_State == "True") // checks if puzzle is done and if so, gives  new dialog
        {
            dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogB.dialogText;
        }
    }
}
