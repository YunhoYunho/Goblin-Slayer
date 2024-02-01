using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    [SerializeField]
    private CinemachineFreeLook freeLookJoyStick;
    [SerializeField]
    private CinemachineFreeLook freeLookKeyBoard;
    [SerializeField]
    private GameObject joyStickUI;
    [SerializeField]
    private GameObject camControlUI;
    [SerializeField]
    private GameObject attackButtonUI;

    public bool isMobile = true;
    public bool isPaused = false;
    public bool isOpenInventory = false;
    public bool isOpenShop = false;
    public bool canMove = true;

    public void OnClickChangeDevice()
    {
        isMobile = !isMobile;

        joyStickUI.SetActive(isMobile);
        camControlUI.SetActive(isMobile);
        attackButtonUI.SetActive(isMobile);
        freeLookJoyStick.Priority = isMobile ? 1 : 0;
        freeLookKeyBoard.Priority = !isMobile ? 1 : 0;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
