using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private int index;
    [SerializeField]
    private string levelName;
    [SerializeField]
    private bool locked;
    [SerializeField]
    private BotType botType;
    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private float time;

    public int Index { get { return index; } private set { } }
    public string LevelName { get { return levelName; } private set { } }
    public bool Locked { get { return locked; } set { locked = value; } }
    public BotType BotType { get { return botType; } private set { } }
    public WeaponType WeaponType { get { return weaponType; } private set { } }
    public float Time { get { return time; } private set { } }
}
