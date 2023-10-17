using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;

    public string Listener(string message) // This mehtod is called when ever dialog is shown
    {
        if (dialogBox == true)
        {
            dialogBox.SetActive(false);
        }
        dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
        dialogBox.SetActive(true);
        return message;
    }
}
