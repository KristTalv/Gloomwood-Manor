using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Sigil : MonoBehaviour
{
    // Strings
    public string statusSigil = "";
    public string diaPuzzSigil;
    private string message;
    // Bools
    private bool isClicked = false;
    // Scriptable Objects
    [SerializeField] private ItemScrObj itemScrObj;
    // Scripts
    private PuzzleManager puzzleManager;


    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();

        statusSigil = "Violet";
        puzzleManager.status_Sigil = statusSigil;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puzzle" && hit.transform.name == "Sir_Edward's_grave")
                {
                    isClicked = true;
                    statusSigil = puzzleManager.status_Sigil;
                    if (statusSigil == "Violet")
                    {
                        message = "A dusty grave.";
                    }
                }
                else
                {
                    isClicked = false;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isClicked)
        {
            //dialogManager.Listener(message);
        }
    }
}
