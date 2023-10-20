using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_LetLight : MonoBehaviour
{
    // Strings
    public string statusLight = "";
    public string diaPuzzLetLight;
    //GameObjects
    [SerializeField] private GameObject sceneLights;
    [SerializeField] private GameObject flameParticle;
    
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
                if (hit.transform.tag == "Puzzle")
                {
                    string cursorSprite = mouseController.mouseScriptCursorName;
                    if(cursorSprite == "Icon_Cursor_Lighter")
                    {
                        statusLight = "Green";
                        puzzleManager.status_LetLight = statusLight;
                        diaPuzzLetLight= dialogLetLight2.dialogText;
                        dialogManager.Listener(diaPuzzLetLight);
                        sceneLights.SetActive(true);
                        flameParticle.SetActive(true);
                        Destroy(gameObject);
                    }
                    else
                    {
                        diaPuzzLetLight = dialogLetLight1.dialogText;
                        dialogManager.Listener(diaPuzzLetLight);
                    }
                }
            }
        }
        

    }
}
