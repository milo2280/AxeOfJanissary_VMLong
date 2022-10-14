using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasEnd : UICanvas
{
    [SerializeField]
    private TextMeshProUGUI titleTMP;

    private void OnEnable()
    {
        OnInit();
    }

    private void OnInit()
    {

    }

    public void HomeButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        Close();
    }
}
