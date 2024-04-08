using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D rigid2D;
    protected Animator animator;

    //상태 패턴
    #region StateMachine
    protected StateMachine stateMachine;
    protected Dictionary<Enum, BaseState> states = new Dictionary<Enum, BaseState>();
    #endregion


    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    //추후 문제 생기면 고치면 됨
    public void ChangeState(Enum state)
    {
        int s = Convert.ToInt32(state);
        stateMachine.SetState(states[(PlayerState)s]);
    }
}
