using UnityEngine;

// All the magick numbers are here.
public class Config : MonoBehaviour
{
    // For QuestObject
    public float[] yCordinateArray = {0.7f, 0.6f, 0.5f }; 
    // - CameraController
    public int[] camIndexChanges = { 1, 1, -1, 1, -1 }; 
    public int[] camArray = { 0, 0, 1, 1, 2 };
    // WayPoint Index, the limit for entranse. If condition not met, cant go any furter.
    public int wayPointLimit = 3;
}
