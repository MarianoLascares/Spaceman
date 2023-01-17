using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instancia;
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;

    private void Awake()
    {
        if(instancia == null)
        {
            instancia = this;
        }
    }

    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }
    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }

    public void ShowInGameMenu()
    {
        inGameCanvas.gameObject.SetActive(true);
    }
    public void HideInGameMenu()
    {
        inGameCanvas.gameObject.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }
    public void HideGameOverMenu()
    {
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
