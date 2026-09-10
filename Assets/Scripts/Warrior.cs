using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Warrior : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public int damage = 10;

    // When the warrior dies it will drop a pickup (a runtime-created ItemData assigned to a PickupItem)
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Simple death: spawn a pickup object where the warrior died
        GameObject drop = GameObject.CreatePrimitive(PrimitiveType.Cube);
        drop.name = "SwordPickup";
        drop.transform.position = transform.position + Vector3.forward * 0.5f + Vector3.up * 0.5f;

        Collider col = drop.GetComponent<Collider>();
        if (col != null) col.isTrigger = true;

        Rigidbody rb = drop.GetComponent<Rigidbody>();
        if (rb == null) rb = drop.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        PickupItem pickup = drop.AddComponent<PickupItem>();

        // Create a runtime ItemData so the pickup has metadata (non-persistent)
        ItemData item = ScriptableObject.CreateInstance<ItemData>();
        item.id = "sword_drop_01";
        item.itemName = "Warrior Sword";
        item.icon = null;

        pickup.itemData = item;

        // Simple death visual & removal
        Destroy(gameObject);
    }
}
