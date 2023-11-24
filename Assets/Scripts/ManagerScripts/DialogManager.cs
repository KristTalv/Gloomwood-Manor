using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    // Game Objects
    [SerializeField] private GameObject dialogBox;

    public string Listener(string message) // This mehtod is called when ever dialog is shown
    {
        dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
        dialogBox.SetActive(true);
        return message;
    }
    public void DisableDialogBox()
    {
        dialogBox.SetActive(false);
    }
}
