Added RuntimeBootstrap to create InventoryManager, UI, a test Sword pickup, and a Player at runtime so Play mode contains the necessary objects without modifying existing scene files.

Behavior:
- On entering Play mode, if InventoryManager doesn't exist it is created and Inventory component added.
- A simple Canvas with InventoryPanel, Content (GridLayoutGroup), and an in-memory SlotPrefab are created and wired to InventoryUI.
- A SwordPickup (Cube) with PickupItem is created at (0,1,2) and will add a runtime ItemData to Inventory when picked up.
- If no GameObject tagged "Player" exists, a Capsule tagged "Player" is created so triggers will work for testing.

This allows you to open any scene and press Play — the inventory, pickup, and basic UI will be created automatically at runtime for testing.
