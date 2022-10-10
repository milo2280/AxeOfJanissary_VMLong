using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Sprite[] backgrounds;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        GameManager.Instance.OnStateChange += ChangeBG;
    }

    private void ChangeBG(GameState state)
    {
        if (state == GameState.MainMenu)
        {
            spriteRenderer.sprite = backgrounds[0];
        }
        else if (state == GameState.Gameplay)
        {
            spriteRenderer.sprite = backgrounds[LevelManager.Instance.CurrentLevelData.Index];
        }
    }
}
