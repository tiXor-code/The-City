using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TheCity.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButton;

    public GameObject ui;

    private string menuSceneName = "0MainMenu";

    public SceneFader sceneFader;

    private bool isFastForwardToggled = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))// || pauseButton.onClick())
        {
            Toggle();
        }        
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            pauseButton.SetActive(true);
        }
    }

    public void TogglePause()
    {
        switch (Time.timeScale)
        {
            case 0f: 
                if(!isFastForwardToggled) Time.timeScale = 1f;
                else Time.timeScale = 2f; break;
            case 1f: Time.timeScale = 0f; break;
            case 2f: Time.timeScale = 0f; break;
        }
    }

    public void ToggleFastForward()
    {
        switch (Time.timeScale)
        {
            case 1f: Time.timeScale = 2f; isFastForwardToggled = true; break;
            case 2f: Time.timeScale = 1f; break;
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

    public void Pause()
    {
        TogglePause();
    }

    public void FastForward()
    {
        ToggleFastForward();
    }    


}
