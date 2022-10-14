using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    [SerializeField]
    private LevelButton levelButtonPrefab;
    [SerializeField]
    private MinusHP minusHPPrefab;
    [SerializeField]
    private PlatformData[] platformDatas;
    [SerializeField]
    private MovingPlatform[] movingPlatformPrefabs;

    public LevelButton LevelButtonPrefab { get { return levelButtonPrefab; } private set { } }
    public MinusHP MinusHPPrefab { get { return minusHPPrefab; } private set { } }
    public MovingPlatform[] MovingPlatformPrefabs { get { return movingPlatformPrefabs; } private set { } }

    public Platform GetPlatformPrefab(PlatformType type)
    {
        return platformDatas[(int)type].prefab;
    }
}

public enum PlatformType
{
    obstacle = 0,
    spawnPlayer = 1,
    spawnBot = 2,
}

[System.Serializable]
public struct PlatformData
{
    public PlatformType type;
    public Platform prefab;
}

