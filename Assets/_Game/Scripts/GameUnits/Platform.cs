using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Transform myTransform;

    public Transform Transform { get { return myTransform; } private set { } }

    private void OnEnable()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                spriteRenderer.enabled = false;
                break;

            case GameState.Gameplay:
                spriteRenderer.enabled = true;
                MatchLevelColor();
                break;

            default:
                break;
        }
    }

    private void MatchLevelColor()
    {
        LevelData data = LevelManager.Instance.CurrentLevelData;
        switch (data.Index)
        {
            case 1:
                spriteRenderer.color = Color.white;
                break;
            case 2:
                spriteRenderer.color = Color.blue;
                break;
            case 3:
                spriteRenderer.color = Color.red;
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChange -= OnGameStateChange;
        }
    }
}
