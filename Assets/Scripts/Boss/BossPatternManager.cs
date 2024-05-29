using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    // 현재 보스 종류
    NowBoss nowBoss = NowBoss.Null;
    // 패턴이 시작되는 시간
    public float patternTime = 5f;
    // 스톱워치와 같은 타이머, 초기값 0
    public float patternTimer = 0f;
    // 단위 거리
    public float unitDis = 0.1f;

    private void Start()
    {
        
    }
    public void GetTargetPattern()
    {
        
    }
    public void GetTargetBoss()
    {

    }
}
