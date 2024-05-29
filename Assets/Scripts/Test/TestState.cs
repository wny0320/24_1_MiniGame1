using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : BaseState
{
    public TestState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null)
        : base(controller, rb, animator, stat)
    {

    }

    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }

    public IEnumerator TestCo()
    {
        Debug.Log(1);
        yield return new WaitForSeconds(1f);
        Debug.Log(2);
    }
}
