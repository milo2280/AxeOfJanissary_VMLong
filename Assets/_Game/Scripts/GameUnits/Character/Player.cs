using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    private bool isAttacking;

    private void Update()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay)) return;
        HandleInput();
    }

    public override void OnInit()
    {
        animController.ChangeAnim(CharacterAnim.idle);
        LevelManager.Instance.PlayerData = data;
        weapon.WeaponType = DataManager.Instance.CurrentPlayerWeapon;
        base.OnInit();
    }

    private void HandleInput()
    {
        if (!isGrounded || IsMouseOverUI()) return;

        if (weapon.IsRespawning)
        {
            if (Input.GetMouseButtonDown(0)) Jump();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdTime = 0;
                isAttacking = true;
                forceBar.gameObject.SetActive(true);
                animController.ChangeAnim(CharacterAnim.charge);
            }

            if (isAttacking && Input.GetMouseButton(0))
            {
                holdTime += Time.deltaTime;
                forcePercent = Mathf.Clamp01(holdTime / MAX_HOLD_TIME);
                if (!Mathf.Approximately(forcePercent, 1f))
                {
                    forceBar.UpdateFill(forcePercent);
                }
            }

            if (isAttacking && Input.GetMouseButtonUp(0))
            {
                isAttacking = false;
                Attack(forcePercent, 1);
                forceBar.gameObject.SetActive(false);
                animController.ChangeAnim(CharacterAnim.idle);
            }
        }
    }

    protected override void Attack(float forcePercent, int side)
    {
        weapon.Attack(forcePercent, side);
    }

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        CameraManager.Instance.ShakeCamera(10f, 0.2f);
        LevelManager.Instance.UpdateHP(Side.player, currentHP / data.HP);
    }

    private bool IsMouseOverUI()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            for (int i = 0; i < raycastResults.Count; i++)
            {
                if (raycastResults[i].gameObject.CompareTag(Constant.TAG_BUTTON))
                {
                    return true;
                }
            }

            return false;
        }

        return true;
    }
}
