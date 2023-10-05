using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject entransePoints; // Camera positions and angles
    [SerializeField] private GameObject centralPoints;
    [SerializeField] private GameObject exitPoints;

    [SerializeField] private float moveSpeed; // Transition speed
    [SerializeField] private float rotateSpeed;

    Vector3 entranseVector; // Camera positions in cordinates
    Vector3 centralVector;
    Vector3 exitVector;

    List<Vector3> cameraPositions = new List<Vector3>(); // List of way points witch player  walk on
    List<GameObject> cameraGameOjects = new List<GameObject>(); // List of way points witch player  walk on
    [SerializeField] private int cam_I; // index for the list

    public bool camMoveBool; // Bool for moving
    public string wallName = "";
    private int callCaunter = 0;

    void Start()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>(); // Camera needs information of player movements

        entranseVector = entransePoints.transform.position;
        centralVector = centralPoints.transform.position;
        exitVector = exitPoints.transform.position;

        cameraPositions.Add(entranseVector);
        cameraPositions.Add(centralVector);
        cameraPositions.Add(exitVector);

        cameraGameOjects.Add(entransePoints);
        cameraGameOjects.Add(centralPoints);
        cameraGameOjects.Add(exitPoints);

        // Un comment below if want to see camera positions in the list
        //foreach (Vector3 item in cameraPositions)
        //{
        //    Debug.Log("Kamera koordinaatit: " + item.x + "x " + item.y + "y " + item.z + "z ");
        //}
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
        if (wallName == "Collision_A" && cam_I == 1 && callCaunter == 0)
        {
            callCaunter++;
            cam_I--;
            Debug.Log(cam_I);

        }
        if (wallName == "Collision_A" && cam_I == 0 && callCaunter == 0)
        {
            callCaunter++;
            cam_I++;
            Debug.Log(cam_I);
        }
        if (wallName == "Collision_B" && cam_I == 1 && callCaunter == 0)
        {
            callCaunter++;
            cam_I++;
            Debug.Log(cam_I);

        }
        if (wallName == "Collision_B" && cam_I == 2 && callCaunter == 0)
        {
            callCaunter++;
            cam_I--;
            Debug.Log(cam_I);

        }
        targetPosition = cameraPositions[cam_I];
        Quaternion targetRotation = Quaternion.LookRotation(cameraGameOjects[cam_I].transform.forward, Vector3.up);

        // Siirr� kameraa EntrancePointiin
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // K��nny kohti EntrancePointia
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Jos kamera on l�hell� EntrancePointia, pys�yt� liike
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            callCaunter = 0;
            camMoveBool = false;
        }
    }
}
