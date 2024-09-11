using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundCollector")]
public class SoundCollector : ScriptableObject
{
    //���ʿ� ���� ������ ��
    public AudioClip testclip;

    public AudioClip DodgeClip;


    [Header("�ѱ� ����")]
    public AudioClip ReloadingClip;
    public AudioClip PistolClip;
    public AudioClip RifleClip;
    public AudioClip SniperClip;
}
