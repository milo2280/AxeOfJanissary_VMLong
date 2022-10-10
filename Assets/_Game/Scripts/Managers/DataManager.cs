using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    private PlayerTypeAndData[] playerTypeAndDatas;
    [SerializeField]
    private BotTypeAndData[] botTypeAndDatas;
    [SerializeField]
    private WeaponTypeAndData[] weaponTypeAndDatas;

    private CharacterType currentPlayerCharacter;
    private WeaponType currentPlayerWeapon;

    public const int PLAYER_CAN_USE = 6;

    public CharacterType CurrentPlayerCharacter { get { return currentPlayerCharacter; } set { currentPlayerCharacter = value; } }
    public WeaponType CurrentPlayerWeapon { get { return currentPlayerWeapon; } set { currentPlayerWeapon = value; } }
    public int PlayerAmount { get { return playerTypeAndDatas.Length; } private set { } }
    public int BotAmount { get { return botTypeAndDatas.Length; } private set { } }
    public int PlayerWeaponAmount { get { return weaponTypeAndDatas.Length; } private set { } }

    public CharacterData GetPlayerData(CharacterType type)
    {
        return playerTypeAndDatas[(int)type].data;
    }

    public CharacterData GetBotData(BotType type)
    {
        return botTypeAndDatas[(int)type].data;
    }

    public WeaponData GetWeaponData(WeaponType type)
    {
        return weaponTypeAndDatas[(int)type].data;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("character", (int)currentPlayerCharacter);
        PlayerPrefs.SetInt("weapon", (int)currentPlayerWeapon);
    }

    public void LoadData()
    {
        currentPlayerCharacter = (CharacterType)PlayerPrefs.GetInt("character");
        currentPlayerWeapon = (WeaponType)PlayerPrefs.GetInt("weapon");
    }
}

public enum CharacterType
{
    blueKnight = 0,
    redKnight = 1,
    maleWizard = 2,
}

public enum BotType
{
    bot1 = 0,
    bot2 = 1,
    boss1 = 2,
}

public enum WeaponType
{
    axe = 0,
    sword1 = 1,
    sword2 = 2,
    knife = 3,
    mace = 4,
    staff = 5,
    monster = 6,
}

[System.Serializable]
public struct PlayerTypeAndData
{
    public CharacterType type;
    public CharacterData data;
}

[System.Serializable]
public struct BotTypeAndData
{
    public BotType type;
    public CharacterData data;
}

[System.Serializable]
public struct WeaponTypeAndData
{
    public WeaponType type;
    public WeaponData data;
}
