using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TestState : BaseState
{
    List<MethodInfo> testList = new List<MethodInfo>();
    Vector3 objPos = Vector3.zero;
    Vector3 targetPos = Vector3.zero;
    float targetMoveSpeed = 1f;
    public TestState(BaseController controller, Rigidbody2D rb = null, Animator animator = null, Stat stat = null)
        : base(controller, rb, animator, stat)
    {

    }

    public override void OnStateEnter()
    {
        
    }


    public override void OnStateUpdate()
    {
        GetCoroutineList();
        Manager.Instance.InvokePattern(this,testList[1]);
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
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log(2);
        yield return Manager.Instance.nowPattern = null;
    }

    public void GetCoroutineList()
    {
        foreach (var method in typeof(TestState).GetMethods())
        {
            if (method.ReturnType == typeof(IEnumerator))
            {
                testList.Add(method);
            }
        }
        //해당 타입에서 모든 메소드를 가져오는 코드
        //https://stackoverflow.com/questions/1198417/generate-list-of-methods-of-a-class-with-method-types
        //foreach (var method in typeof(Boss1PatternState).GetMethods())
        //{
        //    var parameters = method.GetParameters();
        //    var parameterDescriptions = parameters.Select(x => x.ParameterType + " " + x.Name).ToArray();
        //}
    }
    public IEnumerator Pattern1()
    {
        float result = 0f;
        int num = 100;
        float timer = 2f;
        float time = 0f;
        while(timer > time)
        {
            objPos += new Vector3(1, 1);
            time += Time.deltaTime;
            targetPos = Vector3.MoveTowards(targetPos, objPos, targetMoveSpeed * 0.9f);
            result += num / 2 * Time.deltaTime;
            Debug.Log(result);
            yield return null;
        }
        Debug.Log("done");
        yield return Manager.Instance.nowPattern = null;
    }
}
