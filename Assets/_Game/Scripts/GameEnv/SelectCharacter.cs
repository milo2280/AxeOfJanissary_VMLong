using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField]
    private Transform iconParent;
    [SerializeField]
    private GameObject selectObj, selectedObj;

    private GameObject[] usedIcons;
    private GameObject currentIcon;
    private int index;
    private Side side;

    public void OnInit(Side side)
    {
        this.side = side;
        usedIcons = new GameObject[DataManager.Instance.PlayerAmount];

        if (side == Side.left)
        {
            index = (int)DataManager.Instance.CurrentP1;
        }
        else
        {
            index = (int)DataManager.Instance.CurrentP2;
        }

        UpdateIcon();
        UpdateButton();
    }

    private void UpdateIcon()
    {
        if (currentIcon != null) currentIcon.SetActive(false);

        if (usedIcons[index] == null)
        {
            CharacterData data = DataManager.Instance.GetPlayerData((PlayerType)index);
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
        bool selected = false;
        if (side == Side.left)
        {
            selected = index == (int)DataManager.Instance.CurrentP1;
        }
        else
        {
            selected = index == (int)DataManager.Instance.CurrentP2;
        }
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
        if (side == Side.left)
        {
            DataManager.Instance.CurrentP1 = (PlayerType)index;
        }
        else
        {
            DataManager.Instance.CurrentP2 = (PlayerType)index;
        }
        UpdateButton();
    }
}
