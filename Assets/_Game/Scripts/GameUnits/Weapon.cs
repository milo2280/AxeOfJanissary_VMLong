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
    private GameObject[] cacheWeapons;
    private float timer;
    private bool isRespawning, isPlaying;

    private const float RESPAWN_TIME = 2f;

    public bool IsRespawning { get { return isRespawning; } private set { } }

    private void Awake()
    {
        cacheWeapons = new GameObject[DataManager.Instance.WeaponAmount];
    }

    private void OnEnable()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;
    }

    private void Update()
    {
        if (!isPlaying) return;

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

    public void OnInit(WeaponType type)
    {
        isRespawning = false;

        if (weaponData != null && weaponType == type) return;

        weaponType = type;
        int index = (int)weaponType % 100;

        weaponData = DataManager.Instance.GetWeaponData(weaponType);

        if (currentWeapon != null) currentWeapon.SetActive(false);

        if (cacheWeapons[index] == null)
        {
            currentWeapon = Instantiate(weaponData.Weapon, myTransform);
            cacheWeapons[index] = currentWeapon;
        }
        else
        {
            currentWeapon = cacheWeapons[index];
            currentWeapon.SetActive(true);
        }
    }

    public void Attack(float forcePercent, int side)
    {
        SimplePool.Spawn<Bullet>(weaponData.Bullet, bulletPoint.position, bulletPoint.rotation).OnInit(weaponData, forcePercent, side);
        if (currentWeapon != null) currentWeapon.SetActive(false);
        timer = RESPAWN_TIME;
        isRespawning = true;
    }

    public void SetActive(bool active)
    {
        isRespawning = false;
        currentWeapon.SetActive(active);
    }

    private void OnGameStateChange(GameState state)
    {
        isPlaying = state == GameState.Gameplay;
        if (currentWeapon != null) currentWeapon.SetActive(isPlaying);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChange -= OnGameStateChange;
        }
    }
}
