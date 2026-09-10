Inventory feature files and Unity setup instructions

Files added (branch: feature/add-inventory):
- Assets/Scripts/ItemData.cs
- Assets/Scripts/Inventory.cs
- Assets/Scripts/PickupItem.cs
- Assets/Scripts/InventoryUI.cs

Unity setup steps (short):
1. Create the Item asset:
   - In Unity: Assets -> Create -> RPG -> Item (from ItemData). Fill id, itemName (e.g., "Sword"), assign icon.
2. Create the sword prefab:
   - Create a 3D object (Cube) as a placeholder, add PickupItem script and assign the ItemData asset.
   - Ensure the object has a Collider (Is Trigger = true) and a Rigidbody (Is Kinematic = true).
   - Make it a prefab and place on the terrain.
3. Player:
   - Tag your player GameObject as "Player". Ensure player has a Collider and Rigidbody (or CharacterController).
4. Inventory Manager:
   - Create an empty GameObject named "InventoryManager" and attach the Inventory script.
5. UI:
   - Create a Canvas -> Panel for the inventoryPanel.
   - Inside Panel create a Content object with GridLayoutGroup (this will be contentParent).
   - Create a slot prefab with an Image child named "Icon" and a Text child named "Name".
   - Create an empty GameObject in scene and attach InventoryUI; link inventoryPanel, contentParent, and slotPrefab.
6. Play and test:
   - Play, walk player over the sword; item is removed and added to Inventory.Instance.items.
   - Press "I" to open inventory and view items.

Notes:
- This implementation uses simple non-stacking list storage. For stacks, change Inventory to store counts or a dictionary.
- Consider replacing Unity UI Text with TextMeshPro for better visuals.
