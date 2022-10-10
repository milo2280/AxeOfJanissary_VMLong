using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField]
    private Transform[] poolBullets;
    [SerializeField]
    private GameUnit[] bullets;

    private void Awake()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;

        for (int i = 0; i < bullets.Length; i++)
        {
            SimplePool.Preload(bullets[i], 10, poolBullets[i]);
        }
    }

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Win:
            case GameState.Lose:
            case GameState.Draw:
                SimplePool.CollectAll();
                break;
        }
    }
}
