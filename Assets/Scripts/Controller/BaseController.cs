using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour, IReceiveAttack
{
    public Stat stat;
    protected Rigidbody2D rigid2D;
    protected Animator animator;

    public GameObject ground; // Global 로 빼는게 나을듯 // 그래보인다

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

    //각 컨트롤러 상태 변경을 위해 오버라이드 ㄱㄱ
    public virtual void ChangeState(Enum state) { }

    public virtual void OnHit(float damage) { }
}
