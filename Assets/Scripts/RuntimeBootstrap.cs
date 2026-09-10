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

        // Create a Warrior NPC under Enviroment/Ground if none exists
        if (GameObject.Find("WarriorNPC") == null)
        {
            // Attempt to parent under Enviroment/Ground if present
            GameObject parent = GameObject.Find("Enviroment");
            Transform groundT = null;
            if (parent != null)
            {
                Transform g = parent.transform.Find("Ground");
                if (g != null) groundT = g;
            }

            GameObject warriorGO = new GameObject("WarriorNPC");
            if (groundT != null) warriorGO.transform.SetParent(groundT, false);
            warriorGO.transform.position = new Vector3(2f, 1f, 2f);

            // Create a simple visual: capsule body and cube head
            GameObject body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            body.name = "Body";
            body.transform.SetParent(warriorGO.transform, false);
            body.transform.localPosition = Vector3.zero;
            body.transform.localScale = new Vector3(0.9f, 1.0f, 0.9f);

            GameObject head = GameObject.CreatePrimitive(PrimitiveType.Cube);
            head.name = "Head";
            head.transform.SetParent(warriorGO.transform, false);
            head.transform.localPosition = new Vector3(0f, 1.1f, 0f);
            head.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Add collider & rigidbody for physics (so pickup collisions and other physics work around it)
            Collider wcol = warriorGO.AddComponent<SphereCollider>();
            (wcol as SphereCollider).radius = 0.6f;
            Rigidbody wrb = warriorGO.AddComponent<Rigidbody>();
            wrb.isKinematic = true;

            // Add warrior components
            Warrior warrior = warriorGO.AddComponent<Warrior>();
            WarriorAI ai = warriorGO.AddComponent<WarriorAI>();
            ai.detectionRadius = 8f;
            ai.speed = 2f;
            ai.attackRange = 1.2f;
            ai.attackCooldown = 1.2f;
        }
    }
}
