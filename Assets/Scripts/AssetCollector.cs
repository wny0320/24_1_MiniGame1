using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetCollector : MonoBehaviour
{
    //����, ����Ʈ ���� ��
    public SoundCollector sound;

    private void Awake()
    {
        Global.Sound = sound;
    }
}
