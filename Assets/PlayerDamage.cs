using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {


    private int _playerAttackStateHash = Animator.StringToHash("Attack.Attack1");
    
    Animator _animator;
    public int Damage;
    EnemyHealthGUI enemyHealthGUI;
    public GameObject Orc;
    bool Hit;
	
	void Awake ()
    {
        _animator = GetComponentInParent<Animator>();
        enemyHealthGUI = Orc.GetComponent<EnemyHealthGUI>();
        Orc = GameObject.FindGameObjectWithTag("Orc");       
	}

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Orc)
        {
            Hit = true;
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            
            if (info.fullPathHash == _playerAttackStateHash && Hit)
            {
                Debug.Log("You did damage to enemy!");
                
                enemyHealthGUI.TakeDamage(Damage);
            }
        }
    }

     void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Orc)
        {
            Hit = false;
        }
    }


}
