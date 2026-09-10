using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    public ItemData itemData;

    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
        else
        {
            var sc = gameObject.AddComponent<SphereCollider>();
            sc.isTrigger = true;
        }

        if (GetComponent<Rigidbody>() == null)
        {
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // so it doesn't fall or react to physics
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Ensure your player GameObject has tag "Player"
        if (other.CompareTag("Player"))
        {
            if (Inventory.Instance != null && itemData != null)
            {
                Inventory.Instance.Add(itemData);
                // Optional: play pickup sound, spawn VFX, etc.
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Inventory.Instance or itemData is null");
            }
        }
    }
}
