using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSelectWeapon : UICanvas
{
    [SerializeField]
    private Image weaponIcon;
    [SerializeField]
    private GameObject selectObj, selectedObj;

    private int index;

    private void OnEnable()
    {
        index = (int)DataManager.Instance.CurrentPlayerWeapon;
        UpdateIcon();
        UpdateButton();
    }

    private void UpdateIcon()
    {
        WeaponData data = DataManager.Instance.GetWeaponData((WeaponType)index);
        weaponIcon.sprite = data.Sprite;
    }

    private void UpdateButton()
    {
        bool selected = index == (int)DataManager.Instance.CurrentPlayerWeapon;
        selectObj.SetActive(!selected);
        selectedObj.SetActive(selected);
    }

    public void NextButton()
    {
        if (index < DataManager.PLAYER_CAN_USE - 1)
        {
            index++;
            UpdateIcon();
            UpdateButton();
        }
    }

    public void PrevButton()
    {
        if (index > 0)
        {
            index--;
            UpdateIcon();
            UpdateButton();
        }
    }

    public void SelectButton()
    {
        DataManager.Instance.CurrentPlayerWeapon = (WeaponType)index;
        UpdateButton();
    }

    public void BackButton()
    {
        UIManager.Instance.OpenUI(UIID.UICMainMenu);
        Close();
    }
}
