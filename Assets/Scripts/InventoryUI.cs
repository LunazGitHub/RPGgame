using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;     // assign the panel GameObject
    public Transform contentParent;       // parent where slots are instantiated (use GridLayoutGroup)
    public GameObject slotPrefab;         // prefab containing Icon (Image) and Name (Text)

    private bool isOpen = false;
    private Inventory inventory;

    void Start()
    {
        inventory = Inventory.Instance;
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            if (inventoryPanel != null) inventoryPanel.SetActive(isOpen);
            if (isOpen) Refresh();
        }
    }

    public void Refresh()
    {
        if (contentParent == null || slotPrefab == null || inventory == null) return;

        // Clear old slots
        foreach (Transform t in contentParent) Destroy(t.gameObject);

        // Populate
        foreach (var item in inventory.items)
        {
            var slot = Instantiate(slotPrefab, contentParent);
            var icon = slot.transform.Find("Icon")?.GetComponent<Image>();
            var nameText = slot.transform.Find("Name")?.GetComponent<Text>();

            if (icon != null) icon.sprite = item.icon;
            if (nameText != null) nameText.text = item.itemName;
        }
    }
}
