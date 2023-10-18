using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // Strings
    public string status_LetLight = "";
    public string status_Sigil = "";
    public string status_Sword = "";
    // Scripts
    private Puz_LetLight puzz_LetLight;
    private Puz_Sigil puzz_Sigil;
    private Puz_Sword puzz_Sword;
    // List
    List<string> puzzleNames = new List<string>();


    private void Start()
    {
        puzz_LetLight = FindObjectOfType<Puz_LetLight>();
        puzz_Sigil = FindObjectOfType<Puz_Sigil>();
        puzz_Sword = FindObjectOfType<Puz_Sword>();
        status_LetLight = puzz_LetLight.statusLight;
        status_Sigil = puzz_Sigil.statusSigil;
        status_Sword = puzz_Sword.statusSword;

    }
    
    public string GivePuzzState(string state)
    {
        Debug.Log(state);
        return state;
    }
}
