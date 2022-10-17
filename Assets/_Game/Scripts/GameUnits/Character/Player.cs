using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    private bool isAttackStarted;

    protected void Update()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay) || isDead) return;

        if (!isGrounded)
        {
            CheckGround();
            return;
        }

        if (isAttackStarted)
        {
            Holding();
        }
    }

    public override void OnInit()
    {
        animController.ChangeAnim(CharacterAnim.idle);
        weapon.OnInit(DataManager.Instance.CurrentWeapon);
        base.OnInit();
    }

    #region Receive Input From Gameplay Canvas

    public void OnPointerDown()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay) || isDead) return;
        if (!isGrounded || IsMouseOverUI()) return;

        if (weapon.IsRespawning)
        {
            Jump();
        }
        else
        {
            StartAttack();
        }
    }

    public void OnPointerUp()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay) || isDead) return;
        if (!isGrounded || IsMouseOverUI()) return;

        if (isAttackStarted)
        {
            FinishAttack();
        }
    }

    #endregion

    #region Attack Logic

    private void StartAttack()
    {
        holdTime = 0;
        isAttackStarted = true;
        forceBar.gameObject.SetActive(true);
        animController.ChangeAnim(CharacterAnim.charge);
    }

    private void Holding()
    {
        holdTime += Time.deltaTime;
        forcePercent = Mathf.Clamp01(holdTime / MAX_HOLD_TIME);
        if (!Mathf.Approximately(forcePercent, 1))
        {
            forceBar.UpdateFill(forcePercent);
        }
    }

    private void FinishAttack()
    {
        isAttackStarted = false;
        Attack(forcePercent);
        forceBar.gameObject.SetActive(false);
        animController.ChangeAnim(CharacterAnim.idle);
    }

    #endregion

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

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        CameraManager.Instance.ShakeCamera(10f, 0.2f);
    }

    protected override void OnGameStateChange(GameState state)
    {
        base.OnGameStateChange(state);
    }
}
