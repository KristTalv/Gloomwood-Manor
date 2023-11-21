using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // Strings
    public string statusLetLight = "";
    public string statusSigil = "";
    public string statusSword = "";
    public string statusLightsOff = "";
    // Scripts
    private Puz_LetLight puzzLetLight;
    private Puz_Sigil puzzSigil;
    private Puz_Sword puzzSword;
    private Puz_LightsOff puzzLightsOff;

    private void Start()
    {
        puzzLetLight = FindObjectOfType<Puz_LetLight>();
        puzzSigil = FindObjectOfType<Puz_Sigil>();
        puzzSword = FindObjectOfType<Puz_Sword>();
        puzzLightsOff = FindObjectOfType<Puz_LightsOff>();

        statusLetLight = puzzLetLight.statusLight;
        statusSigil = puzzSigil.statusSigil;
        statusSword = puzzSword.statusSword;
        statusLightsOff = puzzLightsOff.statusLightsOff;
    }
    
    public string GivePuzzState(string state)
    {
        Debug.Log(state);
        return state;
    }
}
