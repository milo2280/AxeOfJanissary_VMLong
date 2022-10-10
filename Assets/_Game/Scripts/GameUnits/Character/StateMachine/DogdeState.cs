using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogdeState : IState<Bot>
{

    public void OnEnter(Bot t)
    {
        t.EnterDogdeState();
    }

    public void OnExecute(Bot t)
    {
        t.ExecuteDogdeState();
    }

    public void OnExit(Bot t)
    {

    }
}
