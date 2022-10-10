using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform, bulletPoint;

    private WeaponType weaponType;
    private WeaponData weaponData;
    private GameObject currentWeapon;
    private GameObject[] spawnedWeapons;
    private float timer;
    private bool isRespawning;

    private const float RESPAWN_TIME = 2f;

    public bool IsRespawning { get { return isRespawning; } private set { } }
    public WeaponType WeaponType { get { return weaponType; } set { weaponType = value; } }

    private void Awake()
    {
        spawnedWeapons = new GameObject[DataManager.PLAYER_CAN_USE];
    }

    private void OnEnable()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay)) return;
        if (isRespawning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                isRespawning = false;
                if (currentWeapon != null) currentWeapon.SetActive(true);
            }
        }
    }

    public void OnInit()
    {
        isRespawning = false;
        int index = (int)weaponType;
        weaponData = DataManager.Instance.GetWeaponData(weaponType);

        if (weaponData.Weapon != null)
        {
            if (currentWeapon != null) currentWeapon.SetActive(false);

            if (spawnedWeapons[index] == null)
            {
                currentWeapon = Instantiate(weaponData.Weapon, myTransform);
                spawnedWeapons[index] = currentWeapon;
            }
            else
            {
                currentWeapon = spawnedWeapons[index];
                currentWeapon.SetActive(true);
            }
        }
    }

    private void OnGameStateChange(GameState state)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(state == GameState.Gameplay);
        }
    }

    public void Attack(float forcePercent, int side)
    {
        SimplePool.Spawn<Bullet>(weaponData.Bullet, bulletPoint.position, bulletPoint.rotation).OnInit(weaponData, forcePercent, side);
        timer = RESPAWN_TIME;
        isRespawning = true;
        if (currentWeapon != null) currentWeapon.SetActive(false);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChange -= OnGameStateChange;
        }
    }
}
