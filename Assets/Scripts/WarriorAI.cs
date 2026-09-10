using UnityEngine;

[RequireComponent(typeof(Warrior))]
public class WarriorAI : MonoBehaviour
{
    public float detectionRadius = 8f;
    public float speed = 2.5f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;

    private float lastAttackTime = -999f;
    private Transform player;
    private Warrior warrior;

    void Start()
    {
        warrior = GetComponent<Warrior>();
    }

    void Update()
    {
        if (player == null)
        {
            GameObject pgo = GameObject.FindWithTag("Player");
            if (pgo != null) player = pgo.transform;
        }

        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= detectionRadius)
        {
            // Move towards player if not in attack range
            if (dist > attackRange)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
                // face player
                Vector3 look = player.position - transform.position;
                look.y = 0f;
                if (look.sqrMagnitude > 0.001f) transform.rotation = Quaternion.LookRotation(look);
            }
            else
            {
                // Attack (call a TakeDamage on any component on the player named 'PlayerHealth' or similar)
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                    // Try to find a method to damage the player
                    var pgo = player.gameObject;
                    // If player has a script with TakeDamage(int) method, invoke it via SendMessage
                    pgo.SendMessage("TakeDamage", warrior.damage, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("Warrior attacked player for " + warrior.damage + " damage");
                }
            }
        }
    }
}
