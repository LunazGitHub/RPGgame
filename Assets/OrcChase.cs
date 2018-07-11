using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcChase : MonoBehaviour
{

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    Animator animations;
    bool faceTargetIsOn;
    EnemyHealthGUI enemyHealth;
    Rigidbody rigi;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animations = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealthGUI>();
        animations.SetBool("isIdle", true);
        faceTargetIsOn = true;
        rigi = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            animations.SetBool("isWalking", true);
            animations.SetBool("isIdle", false);          
            agent.GetComponent<NavMeshAgent>().enabled = true;         
            agent.SetDestination(target.position);
        }
        else if(distance >= lookRadius)
        {
            animations.SetBool("isWalking", false);
            animations.SetBool("isIdle", true);
            agent.GetComponent<NavMeshAgent>().enabled = false;
        }

        if(distance <= agent.stoppingDistance )
        {
            animations.SetBool("isWalking", false);
            animations.SetBool("isAttacking", true);

            if (enemyHealth.CurrentHealth > 0)
            {
                FaceTarget();
            }    
        }
        else
        {
            animations.SetBool("isAttacking", false);           
        }

       
            
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

	
}
