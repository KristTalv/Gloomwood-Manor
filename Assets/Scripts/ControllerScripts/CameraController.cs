using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Arrays
    private int[] camIndexChanges = { 0, 0, 0, 0, 0 };
    private int[] camArray = { 0, 0, 0, 0, 0 };
    private string[] wallNames = { "Collision_A", "Collision_A", "Collision_A", "Collision_B", "Collision_B" };
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
    public bool isMoveCam; // Bool for moving
    // GameObjects
    [SerializeField] private GameObject entransePoint; // Camera positions and angles
    [SerializeField] private GameObject centralPoint;
    [SerializeField] private GameObject exitPoint;
    // Vector3
    Vector3 entranseVector; // Camera positions in cordinates
    Vector3 centralVector;
    Vector3 exitVector;
    // Lists
    List<Vector3> cameraPositionList = new List<Vector3>(); // List of camer positions
    List<GameObject> cameraGameOjects = new List<GameObject>(); // List of the Game Objects, marking the camera position.
    // Scripts
    private DialogManager dialogManager;
    private Config config;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        config = FindObjectOfType<Config>();

        for (int i = 0; i < camIndexChanges.Length; i++) // setting array values from config class
        {
            camIndexChanges[i] = config.camIndexChanges[i];
            camArray[i] = config.camArray[i];
        }

        entranseVector = entransePoint.transform.position;
        centralVector = centralPoint.transform.position;
        exitVector = exitPoint.transform.position;

        // Making list of Vector3 camera points
        cameraPositionList.Add(entranseVector);
        cameraPositionList.Add(centralVector);
        cameraPositionList.Add(exitVector);

        // Making list of the GameObjects witch are used to make the previous list
        cameraGameOjects.Add(entransePoint);
        cameraGameOjects.Add(centralPoint);
        cameraGameOjects.Add(exitPoint);
    }

    void Update()
    {
        if (isMoveCam)
        {
            MoveCamera();
        }
    }

    public void MoveCamera()
    {
        Vector3 targetPosition;

        for (int i = 0; i < camIndexChanges.Length; i++) // loop for camera pos
        {
            if (wallName == wallNames[i] && callCaunter == 0 && firstTime == 0) // player gets a dialog when camera moves to center for the first time
            {
                firstTime++;
                callCaunter++;
                cam_I += camIndexChanges[i];
                StartCoroutine(DialogEnter());
            }
            else if (wallName == wallNames[i] && callCaunter == 0 && firstTime != 0 && cam_I == camArray[i]) //finds the correct operation from camIndexChange
            {
                callCaunter++;
                cam_I += camIndexChanges[i];
            }
        }

        targetPosition = cameraPositionList[cam_I];
        Quaternion targetRotation = Quaternion.LookRotation(cameraGameOjects[cam_I].transform.forward, Vector3.up);

        // Move Camera
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate Camera
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Stop Camera
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            callCaunter = 0;
            isMoveCam = false;
        }
    }
    IEnumerator DialogEnter()
    {
        yield return new WaitForSeconds(8f);
        dialogManager.Listener("Incredible! The crypt has been preserved wonderfully.");
    }
}
