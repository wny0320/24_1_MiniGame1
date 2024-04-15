using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public bool isMoving = false;
    public bool isDodging = false;
    public bool isParrying = false;

    public Action PlayerMove;
    public Action<Vector2> PlayerDodge;
    public Func<Vector2> GetPlayerDir;

    public void OnUpdate()
    {
        if (!Manager.Game.isAlive) return;

        PlayerMove.Invoke();
    }
}
