using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    
    public static GameManager instance;

    private PlayerController controller;

    public int collectedObject = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)
        {
            StarGame();
        }
        

    }
    public void StarGame()
    {
        SetGameState(GameState.inGame);
        //controller.StartGame();
    }
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.menu:
                MenuManager.instancia.ShowMainMenu();
                MenuManager.instancia.HideGameOverMenu();
                MenuManager.instancia.HideInGameMenu();
                break;
            case GameState.inGame:
                LevelManager.instance.RemoveAllLevelBlocks();
                LevelManager.instance.GenerateInitialLevelBlock();
                controller.StartGame();

                MenuManager.instancia.ShowInGameMenu();
                MenuManager.instancia.HideMainMenu();
                MenuManager.instancia.HideGameOverMenu();
                break;
            case GameState.gameOver:
                MenuManager.instancia.ShowGameOverMenu();
                MenuManager.instancia.HideInGameMenu();
                break;
        }

        /*if(newGameState == GameState.menu)
        {
            MenuManager.instancia.ShowMainMenu();

        } else if(newGameState == GameState.inGame)
        {
            LevelManager.instance.RemoveAllLevelBlocks();
            LevelManager.instance.GenerateInitialLevelBlock();
            controller.StartGame();

            MenuManager.instancia.ShowInGameMenu();
            MenuManager.instancia.HideMainMenu();

        } else if(currentGameState == GameState.gameOver)
        {
            MenuManager.instancia.ShowGameOverMenu();
        }*/

        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}
