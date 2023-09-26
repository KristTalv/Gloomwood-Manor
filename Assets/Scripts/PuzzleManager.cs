using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private Puz_Button puz_Button; // For getting puzzles status
    public string puz_button_State = "--";



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
