using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public bool isPlayerMove = false;
    public Action PlayerMove;

    public void OnUpdate()
    {
        PlayerMove.Invoke();
    }
}
