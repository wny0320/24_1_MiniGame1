using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected BaseController controller { get; private set; }
    protected Rigidbody2D rb;
    protected Animator animator;

    public BaseState(BaseController controller, Rigidbody2D rb = null, Animator animator = null)
    {
        this.controller = controller;
        this.rb = rb;
        this.animator = animator;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnStateExit();
}