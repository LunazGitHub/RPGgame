Warrior NPC and AI

Files added:
- Assets/Scripts/Warrior.cs
- Assets/Scripts/WarriorAI.cs

Behavior:
- Warrior.cs: simple health/damage component. When health reaches zero, it spawns a runtime pickup (SwordPickup) at its position and destroys itself.
- WarriorAI.cs: very simple chase-and-attack AI. It searches for a GameObject tagged "Player", chases when within detectionRadius, and sends a TakeDamage message when in range.

RuntimeBootstrap updated to spawn a WarriorNPC under Enviroment/Ground (if present) so the NPC will be present in Play mode for testing.

Notes:
- The drop item created on death is a runtime ItemData (not an asset). If you want persistent drops tied to project ItemData assets, we can move ItemData into Resources and reference it by name.
- The AI uses SendMessage("TakeDamage", amount) to respect a variety of possible player health implementations. If your player has a specific health script, ensure it implements TakeDamage(int).
