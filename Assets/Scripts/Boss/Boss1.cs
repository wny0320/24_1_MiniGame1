using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Boss1 : Stat
{
    // 스탯 관련은 나중에 한번에, 여기는 일단 패턴만 짜겠음

    //private void patternRandomPick()
    //{
    //    MethodInfo[] methodInfos = Type.GetType(ToString()).GetMethods(BindingFlags.Public | BindingFlags.Instance);
    //    for(int i = 0; i < methodInfos.Length; i++)
    //    {
    //        Debug.Log(methodInfos[i].Name);
    //    }
    //}
    public void pattern0()
    {

    }
    private void Awake()
    {
        //patternRandomPick();
    }
}
