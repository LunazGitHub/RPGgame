using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Creates runtime scene objects so the inventory and a test pickup exist when entering Play mode.
// This avoids needing to modify serialized scene files and works across Unity versions.
public class RuntimeBootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        // Ensure Inventory Manager exists
        if (Inventory.Instance == null)
        {
            GameObject invGo = new GameObject("InventoryManager");
            invGo.AddComponent<Inventory>();
        }

        // Create simple UI if not present
        if (GameObject.Find("UI_Canvas") == null)
        {
            // Canvas
            GameObject canvasGO = new GameObject("UI_Canvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            // EventSystem
            if (Object.FindObjectOfType<EventSystem>() == null)
            {
                GameObject es = new GameObject("EventSystem");
                es.AddComponent<EventSystem>();
                es.AddComponent<StandaloneInputModule>();
            }

            // Inventory Panel
            GameObject panel = new GameObject("InventoryPanel");
            panel.transform.SetParent(canvasGO.transform, false);
            RectTransform panelRT = panel.AddComponent<RectTransform>();
            panelRT.sizeDelta = new Vector2(400, 300);
            Image panelImg = panel.AddComponent<Image>();
            panelImg.color = new Color(0f, 0f, 0f, 0.5f);
            panel.SetActive(false);

            // Content (Grid)
            GameObject content = new GameObject("Content");
            content.transform.SetParent(panel.transform, false);
            RectTransform contentRT = content.AddComponent<RectTransform>();
            contentRT.anchorMin = new Vector2(0f, 0f);
            contentRT.anchorMax = new Vector2(1f, 1f);
            contentRT.offsetMin = new Vector2(10f, 10f);
            contentRT.offsetMax = new Vector2(-10f, -10f);
            GridLayoutGroup grid = content.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(64f, 64f);
            grid.spacing = new Vector2(8f, 8f);

            // Simple slot prefab (in-memory GameObject used as prefab at runtime)
            GameObject slotPrefab = new GameObject("SlotPrefab");
            RectTransform spRT = slotPrefab.AddComponent<RectTransform>();
            spRT.sizeDelta = new Vector2(64f, 64f);
            GameObject iconGO = new GameObject("Icon");
            iconGO.transform.SetParent(slotPrefab.transform, false);
            Image iconImage = iconGO.AddComponent<Image>();
            RectTransform iconRT = iconGO.GetComponent<RectTransform>();
            iconRT.anchorMin = new Vector2(0f, 0.2f);
            iconRT.anchorMax = new Vector2(1f, 1f);
            iconRT.offsetMin = Vector2.zero;
            iconRT.offsetMax = Vector2.zero;

            GameObject nameGO = new GameObject("Name");
            nameGO.transform.SetParent(slotPrefab.transform, false);
            Text nameText = nameGO.AddComponent<Text>();
            nameText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            nameText.text = "";
            nameText.alignment = TextAnchor.LowerCenter;
            nameText.color = Color.white;
            RectTransform nameRT = nameGO.GetComponent<RectTransform>();
            nameRT.anchorMin = new Vector2(0f, 0f);
            nameRT.anchorMax = new Vector2(1f, 0.3f);
            nameRT.offsetMin = Vector2.zero;
            nameRT.offsetMax = Vector2.zero;

            // InventoryUI manager
            GameObject uiManager = new GameObject("InventoryUIManager");
            InventoryUI invUI = uiManager.AddComponent<InventoryUI>();
            invUI.inventoryPanel = panel;
            invUI.contentParent = content.transform;
            invUI.slotPrefab = slotPrefab;
        }

        // Create a sword pickup in the scene if none exists
        if (GameObject.Find("SwordPickup") == null)
        {
            GameObject sword = GameObject.CreatePrimitive(PrimitiveType.Cube);
            sword.name = "SwordPickup";
            sword.transform.position = new Vector3(0f, 1f, 2f);

            Collider col = sword.GetComponent<Collider>();
            if (col != null) col.isTrigger = true;

            Rigidbody rb = sword.AddComponent<Rigidbody>();
            rb.isKinematic = true;

            PickupItem pickup = sword.AddComponent<PickupItem>();

            // Create a runtime ItemData instance and assign
            ItemData item = ScriptableObject.CreateInstance<ItemData>();
            item.id = "sword_01";
            item.itemName = "Sword";
            item.icon = null;

            pickup.itemData = item;
        }

        // If there is no player in the scene, create a simple capsule player tagged "Player"
        if (GameObject.FindWithTag("Player") == null)
        {
            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player";
            player.tag = "Player";
            player.transform.position = new Vector3(0f, 1f, 0f);
            Rigidbody prb = player.AddComponent<Rigidbody>();
            prb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
