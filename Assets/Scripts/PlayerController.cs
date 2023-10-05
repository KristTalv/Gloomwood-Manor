using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speedWalk = 1.0f; // movment speed

    private float heroLocation; // movement positions
    private float heroDestination;
    
    private int firsttime = 0; //counter

    public GameObject[] walkPathPoint; // waypoint Game Objects --> List willbe made out of them

    private Vector3 moveClick; // mouse click to cordinates
    List<Vector3> wayPoints = new List<Vector3>(); // List of way points witch player  walk on

    public CameraController cameraController; // CameraController script. Camera needs to know when player pass trhoug collision --> camMoveBool = true

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>(); // CameraController script is now available

        foreach(GameObject item in walkPathPoint) // wayPoint List is made in the loop.
        {
            wayPoints.Add(item.transform.position);
        }
        firsttime = 0; // counter will be 0 until player does anything.
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
            if (hit.transform.tag != "UISlot" && hit.transform.tag != "UI")
            {
                firsttime++; // Increment the action count
                moveClick = hit.point;
                //moveClick.y = transform.position.y;
                //moveClick.z = transform.position.z;


                //heroDestination = hit.point.x; // Player moves only on the x-axis
                float smallestResult = 100000000;
                int wayPointIndex = 0;

                for(int i = 0; i < wayPoints.Count; i ++)
                {
                    if(i > 0)
                    {
                        float resutl = wayPoints[i].x - hit.point.x;
                        if (Mathf.Abs(resutl) < smallestResult)
                        {
                            smallestResult = resutl;
                            wayPointIndex = i;
                        }
                    }

                }
                //Debug.Log("wayPoint i:" + wayPointIndex);
                heroDestination = walkPathPoint[wayPointIndex].transform.position.x;
                moveClick.z = walkPathPoint[wayPointIndex].transform.position.z;
                moveClick.y = walkPathPoint[wayPointIndex].transform.position.y;
                moveClick.x = heroDestination;
                //Debug.Log("hero destination: " + heroDestination);
                //Debug.Log("pienin erotus: " + smallestResult);
            }
        }
    }

    private void WalkingOn()
    {
        if (firsttime != 0) //if player has not done any actions, there is no movement
        {
            transform.position = Vector3.MoveTowards(transform.position, moveClick, speedWalk * Time.deltaTime);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TriggerCam")
        {
            cameraController.wallName = other.gameObject.name;
            cameraController.camMoveBool = true;
        }
    }
}