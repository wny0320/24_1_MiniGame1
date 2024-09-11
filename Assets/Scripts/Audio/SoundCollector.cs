using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundCollector")]
public class SoundCollector : ScriptableObject
{
    //이쪽에 사운드 넣으면 됨
    public AudioClip testclip;

    public AudioClip DodgeClip;


    [Header("총기 사운드")]
    public AudioClip ReloadingClip;
    public AudioClip PistolClip;
    public AudioClip RifleClip;
    public AudioClip SniperClip;
}
