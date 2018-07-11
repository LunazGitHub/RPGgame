using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackAudio : MonoBehaviour {


    public AudioClip monsterBite;
    public AudioClip footStep;
    public AudioSource audioSo;
    public AudioSource AudioS;

    public void MonsterAttack()
    {

        audioSo.PlayOneShot(monsterBite, 0.3f);
        
    }

    void FootStep()
    {
        AudioS.PlayOneShot(footStep, 1.4f);
    }
}
