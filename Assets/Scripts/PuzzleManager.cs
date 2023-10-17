using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // Strings
    public string puz_LetLight_Status = "";
    public string puz_Sigil_Status = "";
    // Scripts
    private Puz_LetLight puzz_LetLight;
    private Puz_Sigil puzz_Sigil;
    // List
    List<string> puzzleNames = new List<string>();


    private void Start()
    {
        puzz_LetLight = FindObjectOfType<Puz_LetLight>();
        puzz_Sigil = FindObjectOfType<Puz_Sigil>();
        puz_LetLight_Status = puzz_LetLight.statusLight;
        puz_Sigil_Status = puzz_Sigil.statusSigil;

    }
    
    public string GivePuzzState(string state)
    {
        Debug.Log(state);
        return state;
    }
}
