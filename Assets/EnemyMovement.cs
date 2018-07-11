using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;
    NavMeshAgent nav;

    public AudioClip footStep;
    public AudioSource AudioS;

    public float enemyspeed = 1.3f;
    public float distanceBetweenObjects;
    private float distance;
    private float speed = 2.5f;

    EnemyHealthGUI enemyHealthGUI;

    private float distanceBetweenEnemyAndPlayer;

    bool following;
    bool sinking;

   
    Animator animi;
	
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
       
        enemyHealthGUI = GetComponent<EnemyHealthGUI>();
        animi = GetComponent<Animator>();
        following = false;
       
	}
	
    void Follow()
    {
        if(following)
        {
            this.transform.position += transform.forward * enemyspeed * Time.deltaTime;
           
        }
    }

   
	
	void Update ()
    {

        RotateTowardsPlayer();
        Death();
        //nav.SetDestination(player.position);

        if (sinking)
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
        }

    }

    void RotateTowardsPlayer()
    {

       

        Vector3 direction = player.position - this.transform.position;

        float angle = Vector3.Angle(direction, this.transform.forward);

        distance = Vector3.Distance(player.position, this.transform.position);
        distanceBetweenEnemyAndPlayer = Vector3.Distance(this.transform.position, player.position);

        if (Vector3.Distance(player.position, this.transform.position) < distanceBetweenObjects && angle < 180 )
        {
            direction.y = 0;
         
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);

            animi.SetBool("isIdle", false);
        }

        if (distanceBetweenEnemyAndPlayer < 8 && angle < 180)
        {
            Follow();
            following = true;
            animi.SetBool("isWalking", true);
            animi.SetBool("isAttacking", false);
            nav.GetComponent<NavMeshAgent>().enabled = true;

            if (following == true)
            {
                FootStep();
            }


            if (distanceBetweenEnemyAndPlayer < 1.9 && angle < 180)
            {
                following = false;
                animi.SetBool("isAttacking", true);
                animi.SetBool("isWalking", false);

            }

        }

        else
        {
            animi.SetBool("isIdle", true);
            animi.SetBool("isAttacking", false);
            animi.SetBool("isWalking", false);
    
        }
    }

    void Death()
    {
        if (enemyHealthGUI.CurrentHealth == 0 )
        {
            animi.SetTrigger("isDead");
            sinking = true;
            following = false;
            this.enabled = false;
            nav.GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject, 5f);
        }
    }


    void FootStep()
    {
        AudioS.PlayOneShot(footStep);
    }
}
