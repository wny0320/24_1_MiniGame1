using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour, IReceiveAttack
{
    public Stat stat;
    protected Rigidbody2D rigid2D;
    protected Animator animator;

    public GameObject ground; // Global �� ���°� ������ // �׷����δ�

    //���� ����
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

    //�� ��Ʈ�ѷ� ���� ������ ���� �������̵� ����
    public virtual void ChangeState(Enum state) { }

    public virtual void OnHit(float damage) { }
}
