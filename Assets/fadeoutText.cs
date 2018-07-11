using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeoutText : MonoBehaviour
{


    public void FadeText()
    {
        gameObject.GetComponent<Animation>().Play();
    }

   
}
