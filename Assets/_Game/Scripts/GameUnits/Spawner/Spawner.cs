using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField]
    protected Transform parentTransform;

    protected Character[] spawnedCharacters;
    protected Character character;

    protected int index;

    protected void OnEnable()
    {
        LevelManager.Instance.OnInitLevel += OnInit;
    }

    private void OnInit()
    {
        Spawn();
    }

    protected virtual void Spawn()
    {
        character.OnInit();
    }

    protected void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnInitLevel -= OnInit;
        }
    }
}
