using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<GameState> OnStateChange;
    private GameState state;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;

        DataManager.Instance.LoadData();
        ChangeState(GameState.MainMenu);
    }

    public bool IsState(GameState state)
    {
        return this.state == state;
    }

    public void ChangeState(GameState state)
    {
        this.state = state;

        switch (state)
        {
            case GameState.MainMenu:
                HandleMainMenuState();
                break;
            case GameState.Gameplay:
                HandleGameplayState();
                break;
            case GameState.Pause:
                HandlePauseState();
                break;
            case GameState.Win:
                HandleWinState();
                break;
            case GameState.Lose:
                HandleLoseState();
                break;
            case GameState.Draw:
                HandleDrawState();
                break;
            default:
                break;
        }

        OnStateChange?.Invoke(state);
    }

    private void HandleMainMenuState()
    {
        UIManager.Instance.OpenUI(UIID.UICMainMenu);
    }

    private void HandleGameplayState()
    {
        LevelManager.Instance.OnInit();
        UIManager.Instance.OpenUI(UIID.UICGameplay);
    }

    private void HandlePauseState()
    {
    }

    private void HandleWinState()
    {
        UIManager.Instance.OpenUI(UIID.UICWin);
    }

    private void HandleLoseState()
    {
        UIManager.Instance.OpenUI(UIID.UICLose);
    }

    private void HandleDrawState()
    {
        UIManager.Instance.OpenUI(UIID.UICDraw);
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveData();
    }
}

public enum GameState { MainMenu, Gameplay, Pause, Win, Lose, Draw }
