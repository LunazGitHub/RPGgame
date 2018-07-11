using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
   
    public int ManaCostOfAttack;
    private float nextAttack;
    float timer;
    
    public float timeBetweenAttacks;
    public Animator animator;
    PlayerGUI playergui;
    PlayerDamage playerDamage;
    EnemyHealthGUI enemyhealthGUI;
    float duration = 3;
    GameObject Zombie;
  

    void Start ()
    {
        playerDamage = GetComponent<PlayerDamage>();
        playergui = GetComponent<PlayerGUI>();
        Zombie = GameObject.FindGameObjectWithTag("Enemy");
        //enemyhealthGUI = Zombie.GetComponent<EnemyHealthGUI>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        
        AttackKey();
    }

  

    void AttackKey()
    {

        if(Input.GetMouseButtonDown(1) && playergui.CurrentMana >= ManaCostOfAttack && timer > timeBetweenAttacks)
        {
            timer = 0f;
            animator.SetBool("Attack", true);
            
        }
        else if (Input.GetMouseButtonDown(1) && playergui.CurrentMana < ManaCostOfAttack)
        {
                
            Debug.Log("Wait for your mana!");

        }
        else if(Input.GetMouseButtonUp(1))
        {
           
            animator.SetBool("Attack", false);
            //animator.SetBool("Idle", true);
           
        }

     
    }

    void AttackAnimation()
    {
        playergui.AdjustCurrentMana(-ManaCostOfAttack);
    }
    
}
