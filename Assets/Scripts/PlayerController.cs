using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // floats
    public float speed = 1.0f;
    private float heroLocation;
    private float heroDestination;
    // ints
    private int firsttime = 0;
    // Vectors
    private Vector3 moveClick;


    private void Start()
    {
        firsttime = 0;
    }
    void Update()
    {
        heroLocation = gameObject.transform.position.x;
        if (Input.GetMouseButtonDown(0))
        {
            GetDecPoint();

        }

        if (heroDestination != heroLocation)
        {
            WalkingOn();
        }
    }

    private void GetDecPoint()
    {
        // Raycast to check what the mouse click hit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.transform.tag);
            if (hit.transform.tag != "UISlot" && hit.transform.tag != "UI")
            {
                firsttime++; // Increment the action count
                moveClick = hit.point;
                moveClick.z = transform.position.z;
                moveClick.y = transform.position.y;
                heroDestination = hit.point.x; // Player moves only on the x-axis
            }
        }
    }

    private void WalkingOn()
    {
        if (firsttime != 0) //if player has not done any actions, there is no movement
        {
            transform.position = Vector3.MoveTowards(transform.position, moveClick, speed * Time.deltaTime);
        }

    }
}