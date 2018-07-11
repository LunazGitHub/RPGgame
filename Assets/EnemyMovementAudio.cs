using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAudio : MonoBehaviour
{
    public AudioClip footStep;
    public AudioClip monsterSound;
    public AudioSource AudioS;
    public AudioSource AudioSo;


    void FootStep()
    {
        AudioS.PlayOneShot(footStep, 1.4f);       
    }


    void MonsterSound()
    {

        AudioSo.PlayOneShot(monsterSound, 0.3f);
        
        
        
    }


}
