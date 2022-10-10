using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponData", menuName ="Scriptable/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameUnit bullet;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private Vector2 minFlyForce, holdFlyForce;
    [SerializeField]
    private float minSpinForce, holdSpinForce;
    [SerializeField]
    private float damage;

    public GameObject Weapon { get { return weapon; } private set { } }
    public GameUnit Bullet { get { return bullet; } private set { } }
    public Sprite Sprite { get { return sprite; } private set { } }
    public Vector2 MinFlyForce { get { return minFlyForce; } private set { } }
    public Vector2 HoldFlyForce { get { return holdFlyForce; } private set { } }
    public float MinSpinForce { get { return minSpinForce; } private set { } }
    public float HoldSpinForce { get { return holdSpinForce; } private set { } }
    public float Damage { get { return damage; } private set { } }
}
