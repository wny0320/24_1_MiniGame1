using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    //이곳에 전역 이벤트 & 변수 정의
    //Action, Func
    //EX)
    public static Action ChangeWeapon_Q;
    public static Action ChangeWeapon_E;

    public static Action CamShakeSmall;
    public static Action CamShakeMedium;
    public static Action CamShakeLarge;
    public static Action<float, float, int> CamShakeCustom;

    //AssetCollector
    public static SoundCollector Sound;

    //SFX 사용법 : Global.sfx.Play(Global.Sound.testclip);
    public static SoundManager sfx;
}
