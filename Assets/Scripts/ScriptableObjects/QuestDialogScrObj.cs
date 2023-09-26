using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogText", menuName = "DialogText", order = 1)]
public class QuestDialogScrObj : ScriptableObject
{
    public string dialogName;
    [SerializeField, TextArea]
    public string dialogText;
}
