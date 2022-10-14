using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField]
    private Transform playerIconParent;
    [SerializeField]
    private Image weaponIcon;

    private GameObject[] usedPlayerIcons;
    private GameObject currentPlayerIcon;
    private WeaponType currentWeaponType;

    private void Awake()
    {
        usedPlayerIcons = new GameObject[DataManager.Instance.PlayerAmount];
    }

    private void OnEnable()
    {
        UpdatePlayerIcon();
        UpdateWeaponIcon();
    }

    private void UpdatePlayerIcon()
    {
        PlayerType type = DataManager.Instance.CurrentPlayer;
        int index = (int)type;

        if (currentPlayerIcon != null) currentPlayerIcon.SetActive(false);

        if (usedPlayerIcons[index] == null)
        {
            CharacterData data = DataManager.Instance.GetPlayerData(type);
            currentPlayerIcon = Instantiate(data.Icon, playerIconParent);
            usedPlayerIcons[index] = currentPlayerIcon;
        }
        else
        {
            currentPlayerIcon = usedPlayerIcons[index];
            currentPlayerIcon.SetActive(true);
        }
    }

    private void UpdateWeaponIcon()
    {
        if (currentWeaponType != DataManager.Instance.CurrentWeapon)
        {
            currentWeaponType = DataManager.Instance.CurrentWeapon;
            WeaponData data = DataManager.Instance.GetWeaponData(currentWeaponType);
            weaponIcon.sprite = data.Sprite;
        }
    }

    public void StartButton()
    {
        LevelManager.Instance.ChangeMode(GameMode.mode1v1);
        UIManager.Instance.OpenUI(UIID.UICSelectLevel);
        Close();
    }

    public void Start2v2Button()
    {
        LevelManager.Instance.ChangeMode(GameMode.mode2v2);
        GameManager.Instance.ChangeState(GameState.Gameplay);
        Close();
    }

    public void StartPvPButton()
    {
        //LevelManager.Instance.ChangeMode(GameMode.modePvP);
        //GameManager.Instance.ChangeState(GameState.Gameplay);
        UIManager.Instance.OpenUI(UIID.UICSelectCharacterPvP);
        Close();
    }

    public void OptionsButton()
    {

    }

    public void CharacterButton()
    {
        UIManager.Instance.OpenUI(UIID.UICSelectCharacter);
        Close();
    }

    public void WeaponButton()
    {
        UIManager.Instance.OpenUI(UIID.UICSelectWeapon);
        Close();
    }

    public void ExitButton()
    {
        Debug.Log("Exit!!!");
    }
}
