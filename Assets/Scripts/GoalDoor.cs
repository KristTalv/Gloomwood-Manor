using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDoor : MonoBehaviour
{
    // Bools
    public bool inRangeExitDoor = false;
    // Scripts
    private Puz_LightsOff puz_LightsOff;

    public void Start()
    {
        puz_LightsOff = FindObjectOfType<Puz_LightsOff>();
    }

    public void OnTriggerEnter(Collider other)
    {
        inRangeExitDoor = true;
        puz_LightsOff.GiveRangeBool(inRangeExitDoor);
    }
    public void OnTriggerExit(Collider other)
    {
        inRangeExitDoor = false;
        puz_LightsOff.GiveRangeBool(inRangeExitDoor);
    }


}
