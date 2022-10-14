using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public event Action OnHPChange;
    public event Action OnInitLevel;

    [SerializeField]
    private LevelData[] levelDatas;
    [SerializeField]
    private Spawner[] spawners;
    [SerializeField]
    private MovingPlatform[] movingPlatforms;

    private LevelData currentLevelData;
    private Character[] characterRefs;
    private GameMode currentGameMode;
    private EndType endType;

    // Moving platform position
    // 1 vs 1
    private readonly Vector3 MID = new Vector3(0f, -2f, 0f);
    // 2 vs 2
    private readonly Vector3 TOP = Vector3.zero;
    private readonly Vector3 BOT = new Vector3(0f, -4f, 0f);

    public LevelData[] LevelDatas { get { return levelDatas; } private set { } }
    public LevelData CurrentLevelData { get { return currentLevelData; } set { currentLevelData = value; } }
    public Character[] CharacterRefs { get { return characterRefs; } private set { } }
    public GameMode CurrentGameMode { get { return currentGameMode; } private set { } }
    public EndType EndType { get { return endType; } private set { } }

    private void Awake()
    {
        currentLevelData = levelDatas[0];
        characterRefs = new Character[spawners.Length];
    }

    public void ChangeMode(GameMode mode)
    {
        currentGameMode = mode;
    }

    public bool IsMode(GameMode mode)
    {
        return currentGameMode == mode;
    }

    public void ChangeEndType(EndType type)
    {
        endType = type;
    }

    public void OnInit()
    {
        movingPlatforms[1].gameObject.SetActive(IsMode(GameMode.mode2v2));
        movingPlatforms[3].gameObject.SetActive(IsMode(GameMode.mode2v2));

        switch (currentGameMode)
        {
            case GameMode.mode1v1:
                InitMode1v1();
                break;
            case GameMode.mode2v2:
                InitMode2v2();
                break;
            case GameMode.modePvP:
                InitModePvP();
                break;
            default:
                break;
        }

        OnInitLevel?.Invoke();
    }

    #region Init for different mode

    private void InitMode1v1()
    {
        movingPlatforms[0].Transform.localPosition = MID;
        movingPlatforms[2].Transform.localPosition = MID;

        characterRefs[0] = spawners[0].OnInit(Side.left, DataManager.Instance.CurrentPlayer);
        characterRefs[2] = spawners[2].OnInit(Side.right, currentLevelData.BotType);

        characterRefs[0].gameObject.SetActive(true);
        characterRefs[2].gameObject.SetActive(true);

        characterRefs[0].OnInit();
        characterRefs[2].OnInit();

        characterRefs[0].ChangeName("You", Side.left, Color.white);
    }

    private void InitMode2v2()
    {
        characterRefs[0] = spawners[0].OnInit(Side.left, DataManager.Instance.CurrentPlayer);
        characterRefs[1] = spawners[1].OnInit(Side.left, BotType.bot0);
        characterRefs[2] = spawners[2].OnInit(Side.right, BotType.bot1);
        characterRefs[3] = spawners[3].OnInit(Side.right, BotType.boss0);

        for (int i = 0; i < characterRefs.Length; i++)
        {
            characterRefs[i].gameObject.SetActive(true);
            characterRefs[i].OnInit();
        }

        movingPlatforms[0].Transform.localPosition = TOP;
        movingPlatforms[2].Transform.localPosition = TOP;

        movingPlatforms[1].Transform.localPosition = BOT;
        movingPlatforms[3].Transform.localPosition = BOT;
    }

    private void InitModePvP()
    {
        movingPlatforms[0].Transform.localPosition = MID;
        movingPlatforms[2].Transform.localPosition = MID;

        characterRefs[0] = spawners[0].OnInit(Side.left, PlayerType.player0);
        characterRefs[2] = spawners[2].OnInit(Side.right, PlayerType.player1);

        characterRefs[0].gameObject.SetActive(true);
        characterRefs[2].gameObject.SetActive(true);

        characterRefs[0].OnInit();
        characterRefs[2].OnInit();

        characterRefs[0].ChangeName("P1", Side.left, Color.blue);
        characterRefs[2].ChangeName("P2", Side.right, Color.red);
    }

    #endregion

    public void UpdateHP()
    {
        OnHPChange?.Invoke();
    }

    public void CheckEndLevel()
    {
        bool isLeftWin = false;
        bool isRightWin = false;

        switch (currentGameMode)
        {
            case GameMode.mode1v1:
            case GameMode.modePvP:
                isLeftWin = Mathf.Approximately(characterRefs[2].CurrentHPPercent, 0f);
                isRightWin = Mathf.Approximately(characterRefs[0].CurrentHPPercent, 0f);
                break;

            case GameMode.mode2v2:
                float leftTeamHP = characterRefs[0].CurrentHPPercent + characterRefs[1].CurrentHPPercent;
                float rightTeamHP = characterRefs[2].CurrentHPPercent + characterRefs[3].CurrentHPPercent;
                isLeftWin = Mathf.Approximately(rightTeamHP, 0f);
                isRightWin = Mathf.Approximately(leftTeamHP, 0f);
                break;

            default:
                break;
        }

        if (isLeftWin || isRightWin) 
        {
            if (isLeftWin) endType = EndType.LeftWin;
            if (isRightWin) endType = EndType.RightWin;
            GameManager.Instance.ChangeState(GameState.End);
        }
    }
}

public enum Side { left = 1, right = -1 }
public enum GameMode { mode1v1, mode2v2, modePvP }
public enum EndType { LeftWin, RightWin, Draw }
