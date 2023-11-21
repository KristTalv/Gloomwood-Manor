using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_III : MonoBehaviour
{
    //Strings
    public string statusLetLight;
    // Integers
    private int firstTime = 0; //counter
    // Floats
    public float speedWalk = 1.0f; // movment speed
    private float heroLocation; // movement positions
    private float heroDestination;
    // Bools
    private bool isMoving;
    // Game Objects
    public GameObject[] walkPathPoint; // waypoint Game Objects --> List willbe made out of them
    // Vecrots
    private Vector3 moveClick; // mouse click to cordinates
    // Lists
    List<Vector3> wayPointList = new List<Vector3>(); // List of way points witch player  walk on
    // Scripts
    public CameraController cameraController; // CameraController script. Camera needs to know when player pass trhoug collision --> camMoveBool = true
    private PuzzleManager puzzleManager;
    private DialogManager dialogManager;
    //Animator and AnimationClips
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.GetComponent<Animator>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        cameraController = FindObjectOfType<CameraController>();
        dialogManager = FindObjectOfType<DialogManager>();

        foreach (GameObject item in walkPathPoint) // wayPoint List is made in the loop.
        {
            wayPointList.Add(item.transform.position);
        }
        firstTime = 0; // counter will be 0 until player does anything.
    }
    void Update()
    {
        heroLocation = gameObject.transform.position.x;

        if (Input.GetMouseButtonDown(0))
        {
            statusLetLight = puzzleManager.statusLetLight;
            GetDecPoint();
        }
        if (heroDestination != heroLocation)
        {
            WalkingOn();
        }
        if(isMoving == true && heroLocation == heroDestination)
        {
            isMoving = false;
            animator.SetBool("Walking", false);
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
                isMoving = true;
                animator.SetBool("Walking", true);

                firstTime++; // Increment the action count
                moveClick = hit.point;
                moveClick.y = transform.position.y;

                //float smallestResult = 100000000;
                float smallestResult = float.MaxValue;
                int wayPointIndex = 0;

                for (int i = 0; i < wayPointList.Count; i++) // Calculate the closest way point from mouseclick position
                                                          // --> that point becomes destination heroDestination
                {
                    if (i >= 0)
                    {
                        float resutl = wayPointList[i].x - hit.point.x;
                        if (Mathf.Abs(resutl) < smallestResult)
                        {
                            smallestResult = resutl;
                            wayPointIndex = i;
                        }
                        if (wayPointIndex > 3 && statusLetLight == "Violet")
                        {
                            wayPointIndex = 3;
                            string message = "It's too dark in there";
                            dialogManager.Listener(message);
                        }
                    }
                }

                heroDestination = walkPathPoint[wayPointIndex].transform.position.x;
                moveClick.z = walkPathPoint[wayPointIndex].transform.position.z;

                moveClick.x = heroDestination;
                if(heroLocation != moveClick.x)
                {
                    transform.LookAt(moveClick);
                }
                
            }
        }
    }

    private void WalkingOn()
    {
        if (firstTime != 0) //if player has not done any actions, there is no movement
        {
            transform.position = Vector3.MoveTowards(transform.position, moveClick, speedWalk * Time.deltaTime);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TriggerCam")
        {
            cameraController.wallName = other.gameObject.name;
            cameraController.isMoveCam = true;
        }
    }
}