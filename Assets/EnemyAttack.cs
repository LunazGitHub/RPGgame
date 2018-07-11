using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{

    private int _enemyAttackStateHash = Animator.StringToHash("OrcLayer.AttackOrcSecond");

    public int attackDamage;

    Animator anim;
    
    public GameObject player;
    EnemyHealthGUI enemyHealthGUI;
    PlayerGUI playerGUI;
    BoxCollider box;
    bool playerInRange;
    float timer;
	
	void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        box = GetComponent<BoxCollider>();
        playerGUI = player.GetComponent<PlayerGUI>();
        anim = GetComponentInParent<Animator>();
        enemyHealthGUI = GetComponent<EnemyHealthGUI>();
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            
            if (info.fullPathHash == _enemyAttackStateHash && playerInRange == true)
            {     
                    Debug.Log("Enemy hits you!");
                    playerGUI.TakeDamage(attackDamage);   
            }     
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            
        }
    }


   
}
