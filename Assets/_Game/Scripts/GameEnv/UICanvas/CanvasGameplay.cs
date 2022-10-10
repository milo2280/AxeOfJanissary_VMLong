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
    private RectTransform playerHP, botHP;
    [SerializeField]
    private Image playerAvatar, botAvatar;
    [SerializeField]
    private GameObject pauseObj, continueObj;

    private bool isPause;
    private float timer;

    private void OnEnable()
    {
        GameManager.Instance.OnStateChange += ChangeCanvas;
        LevelManager.Instance.OnHPChange += UpdateHP;
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
        continueObj.SetActive(false);
        pauseObj.SetActive(true);
        UpdateHP(Side.player, 1);
        UpdateHP(Side.bot, 1);
        playerAvatar.sprite = LevelManager.Instance.PlayerData.Avatar;
        botAvatar.sprite = LevelManager.Instance.BotData.Avatar;
    }

    private void UpdateClock()
    {
        int minute = Mathf.FloorToInt(timer / 60);
        int second = Mathf.FloorToInt(timer - minute * 60);

        if (timer < 0)
        {
            minute = 0;
            second = 0;

            GameManager.Instance.ChangeState(GameState.Draw);
        }

        clockTMP.text = string.Format("{0:0}:{1:00}", minute, second);
    }

    private void UpdateHP(Side side, float percent)
    {
        if (side == Side.player)
        {
            playerHP.localScale = new Vector3(percent, 1f, 1f);
        }
        else
        {
            botHP.localScale = new Vector3(percent, 1f, 1f);
        }
    }

    private void ChangeCanvas(GameState state)
    {
        if (state != GameState.Gameplay)
        {
            Close();
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        isPause = true;
        continueObj.SetActive(true);
        pauseObj.SetActive(false);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        isPause = false;
        continueObj.SetActive(false);
        pauseObj.SetActive(true);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChange -= ChangeCanvas;
        }

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnHPChange -= UpdateHP;
        }
    }
}
