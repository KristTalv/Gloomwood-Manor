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
    // Bool
    private bool isGameOverCalled = false;
    private bool isVictoryCalled = false;
    // Scripts
    private Puz_LetLight puzzLetLight;
    private Puz_Sigil puzzSigil;
    private Puz_Sword puzzSword;
    private Puz_LightsOff puzzLightsOff;
    private ScreenManager screenManager;

    private void Start()
    {
        puzzLetLight = FindObjectOfType<Puz_LetLight>();
        puzzSigil = FindObjectOfType<Puz_Sigil>();
        puzzSword = FindObjectOfType<Puz_Sword>();
        puzzLightsOff = FindObjectOfType<Puz_LightsOff>();
        screenManager = FindObjectOfType<ScreenManager>();

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
    private void Update()
    {
        if(statusLightsOff == "Red" && !isGameOverCalled)
        {
            screenManager.isScreenOn[1] = true;
            screenManager.isScreenGameOver = true;
            screenManager.StarGameOver(); // Shows Game Over Screen
            isGameOverCalled = true;

        }
        if(statusLightsOff == "Green" && !isVictoryCalled)
        {
            screenManager.isScreenVictory = true;
            screenManager.isScreenOn[2] = true;
            screenManager.StartVictory(); // Shows Victory Screen
            isVictoryCalled = true;
        }
    }
}
