using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Strings
    public string wallName = "";
    // Integers
    [SerializeField] private int cam_I; // index for the list
    private int callCaunter = 0;
    private int firstTime = 0;
    // Floats
    [SerializeField] private float moveSpeed; // Transition speed
    [SerializeField] private float rotateSpeed;
    // Bools
    public bool camMoveBool; // Bool for moving
    // GameObjects
    [SerializeField] private GameObject entransePoints; // Camera positions and angles
    [SerializeField] private GameObject centralPoints;
    [SerializeField] private GameObject exitPoints;
    // Vector3
    Vector3 entranseVector; // Camera positions in cordinates
    Vector3 centralVector;
    Vector3 exitVector;
    // Lists
    List<Vector3> cameraPositions = new List<Vector3>(); // List of way points witch player  walk on
    List<GameObject> cameraGameOjects = new List<GameObject>(); // List of way points witch player  walk on
    // Scripts
    private DialogManager dialogManager;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();

        entranseVector = entransePoints.transform.position;
        centralVector = centralPoints.transform.position;
        exitVector = exitPoints.transform.position;

        // Making list of Vector3 camera points
        cameraPositions.Add(entranseVector);
        cameraPositions.Add(centralVector);
        cameraPositions.Add(exitVector);

        // Making list of the GameObjects witch are used to make the previous list
        cameraGameOjects.Add(entransePoints);
        cameraGameOjects.Add(centralPoints);
        cameraGameOjects.Add(exitPoints);
    }

    void Update()
    {
        if (camMoveBool)
        {
            MoveCamera();
        }
    }

    public void MoveCamera()
    {
        Vector3 targetPosition;
        if (wallName == "Collision_A" && cam_I == 0 && callCaunter == 0 && firstTime == 0)
        {
            callCaunter++;
            cam_I++;
            firstTime++;
            StartCoroutine(DialogEnter());
            
        }
        if (wallName == "Collision_A" && cam_I == 0 && callCaunter == 0)
        {
            callCaunter++;
            cam_I++;
            firstTime++;
        }
        if (wallName == "Collision_A" && cam_I == 1 && callCaunter == 0)
        {
            callCaunter++;
            cam_I--;
        }
        if (wallName == "Collision_B" && cam_I == 1 && callCaunter == 0)
        {
            callCaunter++;
            cam_I++;
        }
        if (wallName == "Collision_B" && cam_I == 2 && callCaunter == 0)
        {
            callCaunter++;
            cam_I--;
        }
        targetPosition = cameraPositions[cam_I];
        Quaternion targetRotation = Quaternion.LookRotation(cameraGameOjects[cam_I].transform.forward, Vector3.up);

        // Move Camera
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate Camera
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Stop Camera
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            callCaunter = 0;
            camMoveBool = false;
        }
    }
    IEnumerator DialogEnter()
    {
        yield return new WaitForSeconds(8f);
        dialogManager.Listener("Unbelievable! The crypt has been preserved wonderfully.");
    }


}
