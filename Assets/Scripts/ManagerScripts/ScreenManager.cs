using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    private int counter = 0;
    private string storyText = "";

    public bool isScreenStrart = true;
    public bool isScreenGameOver = false;
    public bool isScreenVictory = false;
    public bool[] isScreenOn = {true, false, false};

    [SerializeField] private GameObject blackScreen;

    [SerializeField] private GameObject screenImage;
    [SerializeField] private QuestDialogScrObj[] screenStartText;
    [SerializeField] private QuestDialogScrObj[] screenGameOverText;
    [SerializeField] private QuestDialogScrObj[] screenVictoryText;

    void Start()
    {
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
                    for (int i = 0; i < isScreenOn.Length; i++)
                    {
                        if(isScreenOn[i] == true)
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
                counter = 0;
                screenImage.SetActive(false);
                isScreenOn[index] = false;
            }
        }
        else if (index > 0) // Game Over and Vicotry
        {
            if (index == 1)
            {
                if (counter < screenGameOverText.Length)
                {
                    storyText = screenGameOverText[counter].dialogText;
                    DisplayText(storyText);
                    screenImage.SetActive(true);
                }
                else if (counter >= screenStartText.Length)
                {
                    counter = 0;
                    isScreenOn[index] = false;
                    ReloadScene();
                }
            }
            else if (index == 2)
            {
                if (counter < screenVictoryText.Length)
                {
                    storyText = screenVictoryText[counter].dialogText;
                    DisplayText(storyText);
                    screenImage.SetActive(true);
                }
                else if (counter >= screenStartText.Length)
                {
                    counter = 0;
                    screenImage.SetActive(false);
                    isScreenOn[index] = false;
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
        storyText = screenGameOverText[counter].dialogText;
        DisplayText(storyText);
        screenImage.SetActive(true);
    }

    public void StartVictory()
    {
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
