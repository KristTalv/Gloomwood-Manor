using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Button : MonoBehaviour
{
    public bool questDone = false;
    private bool isClicked_Q = false; // IS needed becaus when entering collision area, players lates click has to be on the "button" (Sphere)

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Sphere")
                {

                    isClicked_Q = true;
                }
                else
                {
                    isClicked_Q = false;
                }
            }
        }
    }

    // Here is the logick for the puzzle
    private void OnTriggerEnter(Component other)
    {
        if (isClicked_Q == true)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            questDone = true;

        }
    }

}
