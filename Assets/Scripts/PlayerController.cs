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
        firsttime = +1; //Caunt that player has done an action
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            moveClick = hit.point;
            moveClick.z = transform.position.z;
            moveClick.y = transform.position.y;
            heroDestination = hit.point.x; // player moves only on x

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