using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    private IState<Bot> currentState;

    private float timer, randomHoldTime;

    private void Update()
    {
        if (!GameManager.Instance.IsState(GameState.Gameplay)) return;

        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        ChangeState(new IdleState());
        LevelManager.Instance.BotData = data;
        weapon.WeaponType = LevelManager.Instance.CurrentLevelData.WeaponType;
        base.OnInit();
    }

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        LevelManager.Instance.UpdateHP(Side.bot, currentHP / data.HP);
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    protected override void Attack(float forcePercent, int side)
    {
        weapon.Attack(forcePercent, side);
    }

    #region Idle State

    public void EnterIdleState()
    {
        timer = Random.Range(0.5f, 1.5f);
    }

    public void ExecuteIdleState()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                ChangeState(new DogdeState());
            }
        }
    }

    #endregion

    #region Dogde State
    public void EnterDogdeState()
    {
        timer = Random.Range(0.2f, 1f); ;
    }

    public void ExecuteDogdeState()
    {
        if (!isGrounded) return;

        if (weapon.IsRespawning && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Jump();
            }
        }
        else
        {
            ChangeState(new AttackState());
        }
    }
    #endregion

    #region Attack State
    public void EnterAttackState()
    {
        holdTime = 0;
        randomHoldTime = Random.Range(0f, 1.5f);
        forceBar.gameObject.SetActive(true);
        animController.ChangeAnim(CharacterAnim.charge);
    }

    public void ExecuteAttackState()
    {
        holdTime += Time.deltaTime;
        forcePercent = Mathf.Clamp01(holdTime / MAX_HOLD_TIME);
        if (!Mathf.Approximately(forcePercent, 1f))
        {
            forceBar.UpdateFill(forcePercent);
        }

        if (holdTime > randomHoldTime)
        {
            Attack(forcePercent, -1);
            forceBar.gameObject.SetActive(false);
            animController.ChangeAnim(CharacterAnim.idle);
            ChangeState(new DogdeState());
        }
    }
    #endregion
}
