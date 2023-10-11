using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // Puzzle manager is for communicating and remembering all puzzle statuses

    private Puz_Button puz_Button; // tessting the manager
    public string puz_button_State = "--";

    private Puz_LetLight puzz_LetLight;
    public string puz_LetLight_Status = "--";

    private void Start()
    {
        puz_Button = FindObjectOfType<Puz_Button>();
        puzz_LetLight = FindObjectOfType<Puz_LetLight>();
        puz_LetLight_Status = puzz_LetLight.statusLight;
    }

    void Update()
    {

        //string is_it_done = puz_Button.questDone.ToString(); // delete this when you dont need reference anymore
        //GivePuzzleState(is_it_done); // delete this when you dont need reference anymore

    }
    
    //public string GivePuzzleState(string state) // delete this when you dont need reference anymore
    //{
    //    puz_button_State = state;
    //    return state;
    //}
}
