using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelectLevel : UICanvas
{
    [SerializeField]
    private Transform selectLevelPanel;

    private void Awake()
    {
        foreach (LevelData data in LevelManager.Instance.LevelDatas)
        {
            LevelButton levelButton = Instantiate<LevelButton>(PrefabManager.Instance.LevelButtonPrefab, selectLevelPanel);
            levelButton.OnInit(data);
            levelButton.ButtonComponent.onClick.AddListener(delegate { SelectLevel(data.Index); });
        }
    }

    private void SelectLevel(int level)
    {
        LevelData data = LevelManager.Instance.LevelDatas[level - 1];
        if (!data.Locked)
        {
            LevelManager.Instance.CurrentLevelData = data;
            GameManager.Instance.ChangeState(GameState.Gameplay);
            Close();
        }
    }

    public void BackButton()
    {
        UIManager.Instance.OpenUI(UIID.UICMainMenu);
        Close();
    }
}
