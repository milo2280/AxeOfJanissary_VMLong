using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelTMP;
    [SerializeField]
    private Button buttonComponent;
    [SerializeField]
    private GameObject lockObj;

    public Button ButtonComponent { get { return buttonComponent; } private set { } }

    public void OnInit(LevelData data)
    {
        levelTMP.text = data.Index.ToString();
        lockObj.SetActive(data.Locked);
    }
}
