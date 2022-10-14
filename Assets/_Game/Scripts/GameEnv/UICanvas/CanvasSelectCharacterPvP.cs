using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelectCharacterPvP : UICanvas
{
    [SerializeField]
    SelectCharacter[] selectCharacters;

    private void OnEnable()
    {
        selectCharacters[0].OnInit(Side.left);
        selectCharacters[1].OnInit(Side.right);
    }

    public void FightButton()
    {
        LevelManager.Instance.ChangeMode(GameMode.modePvP);
        GameManager.Instance.ChangeState(GameState.Gameplay);
        Close();
    }

    public void BackButton()
    {
        UIManager.Instance.OpenUI(UIID.UICMainMenu);
        Close();
    }
}
