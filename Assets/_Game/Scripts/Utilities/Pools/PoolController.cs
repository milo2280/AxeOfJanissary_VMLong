using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField]
    private Transform[] poolBullets;
    [SerializeField]
    private GameUnit[] bullets;
    [SerializeField]
    private Transform poolMinusHP;
    [SerializeField]
    private GameUnit minusHP;

    private void Awake()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;

        for (int i = 0; i < bullets.Length; i++)
        {
            SimplePool.Preload(bullets[i], 10, poolBullets[i]);
        }

        SimplePool.Preload(minusHP, 5, poolMinusHP);
    }

    private void OnGameStateChange(GameState state)
    {
        if (state != GameState.Gameplay)
        {
            SimplePool.CollectAll();
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
