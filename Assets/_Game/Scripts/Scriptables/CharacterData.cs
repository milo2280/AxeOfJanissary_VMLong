using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable/Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private Character prefab;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float mp;
    [SerializeField]
    private Sprite avatar;

    public Character Prefab { get { return prefab; } private set { } }
    public GameObject Icon { get { return icon; } private set { } }
    public float HP { get { return hp; } private set { } }
    public float MP { get { return mp; } private set { } }
    public Sprite Avatar { get { return avatar; } private set { } }
}
