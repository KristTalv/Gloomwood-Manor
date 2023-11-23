using UnityEngine;

[CreateAssetMenu(fileName = "DialogText", menuName = "DialogText", order = 1)]
public class QuestDialogScrObj : ScriptableObject
{
    public string dialogName;
    [SerializeField, TextArea(10, 100)]
    public string dialogText;
}
