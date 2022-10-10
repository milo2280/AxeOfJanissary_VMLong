using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWin : UICanvas
{
    public void HomeButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        Close();
    }

    public void RestartButton()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        Close();
    }
}
