using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelectCharacter : UICanvas
{
    [SerializeField]
    private Transform iconParent;
    [SerializeField]
    private GameObject selectObj, selectedObj;

    private GameObject[] usedIcons;
    private GameObject currentIcon;
    private int index;

    private void Awake()
    {
        usedIcons = new GameObject[DataManager.Instance.PlayerAmount];
    }

    private void OnEnable()
    {
        index = (int)DataManager.Instance.CurrentPlayerCharacter;
        UpdateIcon();
        UpdateButton();
    }

    private void UpdateIcon()
    {
        if (currentIcon != null) currentIcon.SetActive(false);

        if (usedIcons[index] == null)
        {
            CharacterData data = DataManager.Instance.GetPlayerData((CharacterType)index);
            currentIcon = Instantiate(data.Icon, iconParent);
            usedIcons[index] = currentIcon;
        }
        else
        {
            currentIcon = usedIcons[index];
            currentIcon.SetActive(true);
        }
    }

    private void UpdateButton()
    {
        bool selected = index == (int)DataManager.Instance.CurrentPlayerCharacter;
        selectObj.SetActive(!selected);
        selectedObj.SetActive(selected);
    }

    public void NextButton()
    {
        if (index < usedIcons.Length - 1)
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
        DataManager.Instance.CurrentPlayerCharacter = (CharacterType)index;
        UpdateButton();
    }

    public void BackButton()
    {
        UIManager.Instance.OpenUI(UIID.UICMainMenu);
        Close();
    }
}
