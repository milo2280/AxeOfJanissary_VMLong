using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.EnterIdleState();
    }

    public void OnExecute(Bot t)
    {
        t.ExecuteIdleState();
    }

    public void OnExit(Bot t)
    {

    }
}
