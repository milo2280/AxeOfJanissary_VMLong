using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public event Action<Side, float> OnHPChange;
    public event Action OnInitLevel;

    [SerializeField]
    private LevelData[] levelDatas;

    private LevelData currentLevelData;
    private CharacterData playerData, botData;

    public LevelData[] LevelDatas { get { return levelDatas; } private set { } }
    public LevelData CurrentLevelData { get { return currentLevelData; } set { currentLevelData = value; } }
    public CharacterData PlayerData { get { return playerData; } set { playerData = value; } }
    public CharacterData BotData { get { return botData; } set { botData = value; } }

    private void Awake()
    {
        currentLevelData = levelDatas[0];
    }

    public void OnInit()
    {
        OnInitLevel?.Invoke();
    }

    public void UpdateHP(Side side, float percent)
    {
        if (percent < 0 || Mathf.Approximately(percent, 0))
        {
            if (side == Side.player)
            {
                GameManager.Instance.ChangeState(GameState.Lose);
            }
            else
            {
                GameManager.Instance.ChangeState(GameState.Win);
            }
        }

        OnHPChange?.Invoke(side, percent);
    }
}

public enum Side { player, bot }
