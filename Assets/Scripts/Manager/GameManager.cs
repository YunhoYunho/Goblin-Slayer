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
}
