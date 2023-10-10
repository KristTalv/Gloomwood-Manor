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



    void Update()
    {
        puz_Button = FindObjectOfType<Puz_Button>();
        string is_it_done = puz_Button.questDone.ToString();
        GivePuzzleState(is_it_done);
    }
    
    public string GivePuzzleState(string state)
    {
        puz_button_State = state;
        return state;
    }
}
