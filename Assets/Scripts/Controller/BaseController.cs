using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D rigid2D;
    protected Animator animator;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public virtual void ChangeState<T>(T state) where T : Enum
    {

    }
}
