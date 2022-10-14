using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    private LayerMask bgLayer;
    [SerializeField]
    private PlayerTypeAndData[] playerTypeAndDatas;
    [SerializeField]
    private WeaponTypeAndData[] weaponTypeAndDatas;
    [SerializeField]
    private BotTypeAndData[] botTypeAndDatas;

    private PlayerType currentPlayer;
    private WeaponType currentWeapon;

    private List<WeaponTypeAndData> playerUsable = new List<WeaponTypeAndData>();
    private List<WeaponTypeAndData> onlyBossUsable = new List<WeaponTypeAndData>();

    public PlayerType CurrentPlayer { get { return currentPlayer; } set { currentPlayer = value; } }
    public WeaponType CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value; } }
    public int PlayerAmount { get { return playerTypeAndDatas.Length; } private set { } }
    public int WeaponAmount { get { return playerUsable.Count; } private set { } }
    public int BotAmount { get { return botTypeAndDatas.Length; } private set { } }
    public LayerMask BGLayer { get { return bgLayer; } private set { } }

    private void Awake()
    {
        int index;

        for (int i = 0; i < weaponTypeAndDatas.Length; i++)
        {
            index = (int)weaponTypeAndDatas[i].type;

            if (index < 100)
            {
                playerUsable.Add(weaponTypeAndDatas[i]);
            }
            else
            {
                onlyBossUsable.Add(weaponTypeAndDatas[i]);
            }
        }
    }

    public CharacterData GetPlayerData(PlayerType type)
    {
        return playerTypeAndDatas[(int)type].data;
    }

    public CharacterData GetBotData(BotType type)
    {
        return botTypeAndDatas[(int)type].data;
    }

    public WeaponData GetWeaponData(WeaponType type)
    {
        int index = (int)type;

        if (index < 100)
        {
            return playerUsable[index].data;
        }
        else
        {
            index = index % 100;
            return onlyBossUsable[index].data;
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("character", (int)currentPlayer);
        PlayerPrefs.SetInt("weapon", (int)currentWeapon);
    }

    public void LoadData()
    {
        currentPlayer = (PlayerType)PlayerPrefs.GetInt("character");
        currentWeapon = (WeaponType)PlayerPrefs.GetInt("weapon");
    }
}

public enum PlayerType
{
    player0 = 0,
    player1 = 1,
    player2 = 2,
}

public enum BotType
{
    bot0 = 0,
    bot1 = 1,
    boss0 = 2,
}

public enum WeaponType
{
    weapon0 = 0,
    weapon1 = 1,
    weapon2 = 2,
    weapon3 = 3,
    weapon4 = 4,
    weapon5 = 5,
    weapon100 = 100,
}

[System.Serializable]
public struct PlayerTypeAndData
{
    public PlayerType type;
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
