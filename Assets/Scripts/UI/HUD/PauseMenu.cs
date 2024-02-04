using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel;

    private const string sceneName = "LoadingScene";

    public void OnClickStartButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickPauseMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf != true ? 1f : 0;
    }
        
    public void OnClickResumeButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickOptionButton()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
