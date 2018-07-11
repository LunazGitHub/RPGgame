using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthGUI : MonoBehaviour {

    public int MaxHealth = 100;
    public int CurrentHealth = 100;
    private float BarLength = 0.0f;
    bool damaged;
    Animator animis;
    float destroyTime = 5f;
    GameObject orc;
    OrcChase orcChase;
    Rigidbody rigi;
    public Transform player;
    NavMeshAgent agent;
    private float distanceBetweenEnemyAndPlayer;
    PlayerDamage playerDamage;
    bool sinking;
    float speed = 0.5f;

   

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BarLength = Screen.width / 8;
        orcChase = GetComponent<OrcChase>();
        orc = GameObject.FindGameObjectWithTag("Orc");

        GUI.enabled = false;

        rigi = GetComponent<Rigidbody>();
        animis = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerDamage = GetComponent<PlayerDamage>();
        
    }
	
	
	void Update ()
    {
        AdjustCurrentHealth(0);

        if (sinking)
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
        }


        
    }

   
    public void OnGUI()
    {       
        float distance = Vector3.Distance(this.transform.position, player.position);

        if ( distance < orcChase.lookRadius)
        {
            GUI.Box(new Rect(1250, 30, 100, 30), "EnemyHealth");
            GUI.Box(new Rect(1350, 30, BarLength, 30), CurrentHealth.ToString("0") + "/" + MaxHealth);
        }
        else
        {
            GUI.enabled = false;
        }    
    }

    /* void ShowGUI()
     {
         //distanceBetweenEnemyAndPlayer = Vector3.Distance(this.transform.position, player.position);
         orcChase.lookRadius = Vector3.Distance(this.transform.position, player.position);

         if (distance < orcChase.lookRadius)
         {
             GUI.enabled = true;

         }
         else
         {
             GUI.enabled = false;
         }
     }*/


    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0)
        {
            animis.SetTrigger("isDead");
            orcChase.GetComponent<OrcChase>().enabled = false;
            agent.GetComponent<NavMeshAgent>().enabled = false;
            
            sinking = true;
            Destroy(orc, destroyTime);          
        }
    }

    public void AdjustCurrentHealth(int adj)
    {
        CurrentHealth += adj;

        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
           
        }

    }
}
