using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float bias = 0.5f;
    [SerializeField] private float mouseRange = 5f;

    private Vector3 offset;
    private Vector3 subPosition;

    private void Awake()
    {
        offset= transform.position;

        Global.CamShakeSmall = ShakeSmall;
        Global.CamShakeMedium = ShakeMedium;
        Global.CamShakeLarge = ShakeLarge;
        Global.CamShakeCustom = Shake;
    }

    private void Update()
    {
        subPosition = offset + (transform.right * ((Input.mousePosition.x / Screen.width - 0.5f)) + 
            (transform.up * (Input.mousePosition.y / Screen.height - 0.5f))) * mouseRange;

        transform.position = Vector3.Lerp(offset, subPosition, bias);
    }

    public void ShakeSmall() => Shake(0.03f, 0.2f, 25);
    public void ShakeMedium() => Shake(0.08f, 0.4f, 25);
    public void ShakeLarge() => Shake(0.12f, 1f, 25);

    public void Shake(float amount, float duration, int vibrato)
    {
        transform.DORewind();
        transform.DOShakePosition(duration, new Vector3(amount, amount, 0), vibrato).OnComplete(() => transform.localPosition = offset);
    }
}