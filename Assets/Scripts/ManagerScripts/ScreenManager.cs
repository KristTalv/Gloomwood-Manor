using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    private int counter = 0;
    private string storyText = "";

    //public bool isScreenStrart = true;
    //public bool isScreenGameOver = false;
    //public bool isScreenVictory = false;
    public bool[] isScreenOnArray = {true, false, false};

    [SerializeField] private GameObject jonathan;
    //[SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject screenImage;

    [SerializeField] private QuestDialogScrObj[] screenStartText;
    [SerializeField] private QuestDialogScrObj[] screenGameOverText;
    [SerializeField] private QuestDialogScrObj[] screenVictoryText;

    void Start()
    {
        jonathan.SetActive(false);
        //uiCanvas.SetActive(false);
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

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Screen")
                {
                    for (int i = 0; i < isScreenOnArray.Length; i++)
                    {
                        if(isScreenOnArray[i] == true)
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
                jonathan.SetActive(true);
                counter = 0;
                screenImage.SetActive(false);
                isScreenOnArray[index] = false;
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
    private void SetScreen()
    {
        DisplayText(storyText);
        screenImage.SetActive(true);
    }

    public void StarGameOver()
    {
        isScreenOnArray[1] = true;
        storyText = screenGameOverText[counter].dialogText;
        DisplayText(storyText);
        screenImage.SetActive(true);
    }

    public void StartVictory()
    {
        isScreenOnArray[2] = true;
        storyText = screenVictoryText[counter].dialogText;
        DisplayText(storyText);
        screenImage.SetActive(true);
    }
    private void DisplayText(string storyText)
    {
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
}
