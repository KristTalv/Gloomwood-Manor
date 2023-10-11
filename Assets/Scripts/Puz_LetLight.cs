using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_LetLight : MonoBehaviour
{
    public string statusLight = "";
    private InvSlot invSlot;


    void Start()
    {
        statusLight = "Violet";
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            invSlot = FindObjectOfType<InvSlot>();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puzzle")
                {
                    string cursorSprite = invSlot.cursorSpriteName;
                    if(cursorSprite == "Icon_Cursor_Lighter")
                    {
                        statusLight = "Green";
                        PuzzleManager puzzleMang = FindObjectOfType<PuzzleManager>();
                        puzzleMang.puz_LetLight_Status = statusLight;
                        Debug.Log("The flames iluminate my way. I can go futher now.");
                    }
                    //Debug.Log(invSlot.cursorSpriteName);
                    //Debug.Log(hit.transform.tag);
                }
            }
        }
        

    }
}
