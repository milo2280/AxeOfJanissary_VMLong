using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasGameplay : UICanvas
{
    [SerializeField]
    private TextMeshProUGUI clockTMP;
    [SerializeField]
    private StatBar[] statBars; 
    [SerializeField]
    private GameObject pauseObj, continueObj, homeObj;
    [SerializeField]
    private GameObject leftSide, rightSide, singlePanel;

    private bool isPause;
    private float timer;

    private void OnEnable()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;
        OnInit();
    }

    private void Update()
    {
        if (!isPause && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateClock();
        }
    }

    private void OnInit()
    {
        timer = LevelManager.Instance.CurrentLevelData.Time;
        UpdateButton();

        statBars[1].gameObject.SetActive(LevelManager.Instance.IsMode(GameMode.mode2v2));
        statBars[3].gameObject.SetActive(LevelManager.Instance.IsMode(GameMode.mode2v2));

        switch (LevelManager.Instance.CurrentGameMode)
        {
            case GameMode.mode1v1:
                statBars[0].OnInit(LevelManager.Instance.CharacterRefs[0], 1f);
                statBars[2].OnInit(LevelManager.Instance.CharacterRefs[2], 1f);

                leftSide.SetActive(false);
                rightSide.SetActive(false);
                singlePanel.SetActive(true);
                break;

            case GameMode.mode2v2:
                leftSide.SetActive(false);
                rightSide.SetActive(false);
                singlePanel.SetActive(true);

                for (int i = 0; i < statBars.Length; i++)
                {
                    statBars[i].OnInit(LevelManager.Instance.CharacterRefs[i], 0.5f);
                }
                break;

            case GameMode.modePvP:
                leftSide.SetActive(true);
                rightSide.SetActive(true);
                singlePanel.SetActive(false);

                statBars[0].OnInit(LevelManager.Instance.CharacterRefs[0], 1f);
                statBars[2].OnInit(LevelManager.Instance.CharacterRefs[2], 1f);
                break;

            default:
                break;
        }
    }

    private void UpdateClock()
    {
        int minute = Mathf.FloorToInt(timer / 60);
        int second = Mathf.FloorToInt(timer - minute * 60);

        if (timer < 0)
        {
            minute = 0;
            second = 0;

            LevelManager.Instance.ChangeEndType(EndType.Draw);
            GameManager.Instance.ChangeState(GameState.End);
        }

        clockTMP.text = string.Format("{0:0}:{1:00}", minute, second);
    }

    private void OnGameStateChange(GameState state)
    {
        if (state != GameState.Gameplay) Close();
    }

    private void UpdateButton()
    {
        continueObj.SetActive(isPause);
        homeObj.SetActive(isPause);
        pauseObj.SetActive(!isPause);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        isPause = true;
        UpdateButton();
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        isPause = false;
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        isPause = false;
        UpdateButton();
    }

    public void LeftPlayerPointerDown()
    {
        (LevelManager.Instance.CharacterRefs[0] as Player).OnPointerDown();
    }

    public void LeftPlayerPointerUp()
    {
        (LevelManager.Instance.CharacterRefs[0] as Player).OnPointerUp();
    }

    public void RightPlayerPointerDown()
    {
        (LevelManager.Instance.CharacterRefs[2] as Player).OnPointerDown();
    }

    public void RightPlayerPointerUp()
    {
        (LevelManager.Instance.CharacterRefs[2] as Player).OnPointerUp();
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChange -= OnGameStateChange;
        }
    }
}
