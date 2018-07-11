using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterAudio : MonoBehaviour
{

    public AudioClip swordSound;
    public AudioSource AudioS;

	public void SwordSound()
    {

        AudioS.PlayOneShot(swordSound);

    }
}
