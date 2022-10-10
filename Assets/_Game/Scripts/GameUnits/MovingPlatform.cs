using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private PlatformType type;
    [SerializeField]
    private Transform[] pointTransforms;
    [SerializeField]
    private float speed;

    private int index;
    private Platform platform;

    private void Awake()
    {
        platform = Instantiate<Platform>(PrefabManager.Instance.GetPlatformPrefab(type), myTransform);
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnInitLevel += OnInit;
    }

    private void OnInit()
    {
        index = 0;
        platform.Transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay)) return;

        MovePlatform();
    }

    private void MovePlatform()
    {
        if (index == pointTransforms.Length) index = 0;

        platform.Transform.position = Vector3.MoveTowards(platform.Transform.position, pointTransforms[index].position, speed * Time.deltaTime);
        
        if ((platform.Transform.position - pointTransforms[index].position).sqrMagnitude < 0.0001f) index++;
    }

    private void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnInitLevel -= OnInit;
        }
    }
}
