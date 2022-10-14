using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private Transform[] pointTransforms;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Platform platform;

    private int index;
    private bool isReachDes;

    public Transform Transform { get { return myTransform; } private set { } }

    private void OnEnable()
    {
        LevelManager.Instance.OnInitLevel += OnInit;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay)) return;

        MovePlatform();
    }

    private void OnInit()
    {
        platform.Transform.localPosition = Vector3.zero;
    }

    private void MovePlatform()
    {
        if (isReachDes)
        {
            index = Random.Range(0, LevelManager.Instance.CurrentLevelData.Index + 1);
        }

        platform.Transform.position = Vector3.MoveTowards(platform.Transform.position, pointTransforms[index].position, speed * Time.deltaTime);
        
        isReachDes = (platform.Transform.position - pointTransforms[index].position).sqrMagnitude < 0.0001f;
    }

    private void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnInitLevel -= OnInit;
        }
    }
}
