using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    // Integers
    private int counter = 0;
    //Floats
    [SerializeField] private float redFlasTime;
    // Strings
    private string storyText = "";
    // Bools
    public bool[] isScreenOnArray = {true, false, false};
    // GameObjects
    [SerializeField] private GameObject jonathan;
    //[SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject redScreen;
    [SerializeField] private GameObject whiteScreen;
    [SerializeField] private GameObject screenImage;
    [SerializeField] private GameObject screenBlocker;
    // QuestDialogScrOj
    [SerializeField] private QuestDialogScrObj[] screenStartText;
    [SerializeField] private QuestDialogScrObj[] jonathanDialogText;
    [SerializeField] private QuestDialogScrObj[] screenGameOverText;
    [SerializeField] private QuestDialogScrObj[] screenVictoryText;
    // SFX
    [SerializeField] private AudioSource slashAudio;
    [SerializeField] private AudioSource lightUpAudio;
    [SerializeField] private AudioSource burningAudio;
    [SerializeField] private AudioSource lockDoorAudio;
    [SerializeField] private AudioSource victoryAmbient;
    [SerializeField] private AudioSource defaultAmbient;
    // Scripts
    private DialogManager dialogManager;
    private InventoryManager_II inventoryManager;

    void Start()
    {
        blackScreen.SetActive(false);
        redScreen.SetActive(false);
        screenBlocker.SetActive(true);

        dialogManager = FindObjectOfType<DialogManager>();
        inventoryManager = FindObjectOfType<InventoryManager_II>();
        jonathan.SetActive(false);
        storyText = screenStartText[0].dialogText;
        DisplayText(storyText);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (hit.transform.gameObject.tag == "Screen")
                {
                    for (int i = 0; i < isScreenOnArray.Length; i++)
                    {
                        if (isScreenOnArray[i] == true)
                        {
                            UpdateScreen(i);
                        }
                    }
                }
            }
        }
  
    }
    private void UpdateScreen(int index)
    {
        if(index == 0) // Start
        {
            if (counter < screenStartText.Length)
            {
                storyText = screenStartText[1].dialogText;
                DisplayText(storyText);
            }
            else if (counter >= screenStartText.Length)
            {
                lockDoorAudio.Play();
                string message = jonathanDialogText[0].dialogText;
                dialogManager.Listener(message);
                jonathan.SetActive(true);
                counter = 0;
                screenImage.SetActive(false);
                isScreenOnArray[index] = false;
                inventoryManager.GiveStartItems();
                StartCoroutine(Blocker());
            }

        }
        else if (index > 0) // Game Over and Vicotry
        {
            if (index == 1)
            {
                if (counter < screenGameOverText.Length) // Game Over
                {
                    storyText = screenGameOverText[counter].dialogText;
                    SetScreen();
                }
                else if (counter >= screenGameOverText.Length)
                {
                    counter = 0;
                    isScreenOnArray[index] = false;
                    ReloadScene();
                }
            }
            else if (index == 2)
            {
                if (counter < screenVictoryText.Length) // Vicotry
                {
                    storyText = screenVictoryText[counter].dialogText;
                    SetScreen();
                }
                else if (counter >= screenVictoryText.Length)
                {
                    counter = 0;
                    screenImage.SetActive(false);
                    isScreenOnArray[index] = false;
                    blackScreen.SetActive(true);
                    Application.Quit();
                }
            }
        }
    }
  IEnumerator Blocker()
    {
        yield return new WaitForSeconds(1.5f);
        screenBlocker.SetActive(false);
    }
    private void SetScreen()
    {
        DisplayText(storyText);
        screenImage.SetActive(true);
    }

    public void StarGameOver()
    {
        
        StartCoroutine(RedFlash());

        inventoryManager.ClearItems();
        jonathan.SetActive(false);
        isScreenOnArray[1] = true;
        storyText = screenGameOverText[counter].dialogText;
        DisplayText(storyText);
        screenImage.SetActive(true);
    }
    IEnumerator RedFlash()
    {
        slashAudio.Play();
        redScreen.SetActive(true);
        yield return new WaitForSeconds(redFlasTime);
        redScreen.SetActive(false);
    }
    IEnumerator WhiteFlas()
    {
        defaultAmbient.Stop();
        victoryAmbient.Play();
        lockDoorAudio.Play();
        whiteScreen.SetActive(true);
        yield return new WaitForSeconds(redFlasTime);
        whiteScreen.SetActive(false);
    }

    public void StartVictory()
    {
        StartCoroutine(WhiteFlas());
        inventoryManager.ClearItems();
        jonathan.SetActive(false);
        isScreenOnArray[2] = true;
        storyText = screenVictoryText[counter].dialogText;
        DisplayText(storyText);
        screenImage.SetActive(true);
    }
    private void DisplayText(string storyText)
    {
        screenImage.transform.GetChild(0).gameObject.SetActive(false);
        screenImage.transform.GetChild(0).gameObject.SetActive(true);
        screenImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = storyText;
        counter++;
    }
    void ReloadScene()
    {
        blackScreen.SetActive(true);
        StartCoroutine(WaithForScreen());

    }
    IEnumerator WaithForScreen()
    {
        yield return new WaitForSeconds(2f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void PlayLightUp()
    {
        lightUpAudio.Play();
        burningAudio.Play();
    }
    public void StopBurningAudio()
    {
        burningAudio.Stop();
    }
    public void TurnPitchAmbient()
    {
        defaultAmbient = defaultAmbient.GetComponent<AudioSource>();
        defaultAmbient.pitch = 1;
    }
}
