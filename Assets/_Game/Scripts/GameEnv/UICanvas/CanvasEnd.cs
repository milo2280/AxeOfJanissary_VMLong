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
        switch (LevelManager.Instance.EndType)
        {
            case EndType.Draw:
                titleTMP.color = Color.gray;
                titleTMP.text = "DRAW";
                break;

            case EndType.LeftWin:
                titleTMP.color = Color.blue;
                switch (LevelManager.Instance.CurrentGameMode)
                {
                    case GameMode.mode1v1:
                    case GameMode.mode2v2:
                        titleTMP.text = "You win";
                        break;
                    case GameMode.modePvP:
                        titleTMP.text = "P1 win";
                        break;
                    default:
                        break;
                }
                break;

            case EndType.RightWin:
                titleTMP.color = Color.red;
                switch (LevelManager.Instance.CurrentGameMode)
                {
                    case GameMode.mode1v1:
                    case GameMode.mode2v2:
                        titleTMP.text = "You lose";
                        break;
                    case GameMode.modePvP:
                        titleTMP.text = "P2 win";
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

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
