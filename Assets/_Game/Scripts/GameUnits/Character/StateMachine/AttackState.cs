using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.EnterAttackState();
    }

    public void OnExecute(Bot t)
    {
        t.ExecuteAttackState();
    }

    public void OnExit(Bot t)
    {
    }
}
