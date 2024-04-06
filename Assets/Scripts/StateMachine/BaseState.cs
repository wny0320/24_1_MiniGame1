using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected PlayerController pc { get; private set; }

    public BaseState(PlayerController pc)
    {
        this.pc = pc;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnStateExit();
}