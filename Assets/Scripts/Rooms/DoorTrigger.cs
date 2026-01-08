
using UnityEngine;
using System.Collections;
public class DoorTrigger : MonoBehaviour
{
    public DoorController parentDoor; // assign parent DoorController in inspector
    public float teleportCooldown = 0.2f; // seconds to prevent instant retrigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (parentDoor == null)
        {
            Debug.LogWarning("ParentDoor not assigned on DoorTrigger!");
            return;
        }
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player == null) return;
        // Prevent instant re-teleport
        if (player.justTeleported) return;
        // Check if the room is cleared before allowing teleport
        if (parentDoor.parentRoom != null && !parentDoor.parentRoom.IsCleared())
        {
            Debug.Log("Room not cleared yet!");
            return;
        }
        Debug.Log("Door triggered, moving player");
        // Teleport the player to the target spawn
        if (parentDoor.targetSpawn != null)
            other.transform.position = parentDoor.targetSpawn.position;
        // Tell RoomManager to activate the next room
        if (parentDoor.manager != null)
            parentDoor.manager.ActivateRoom(parentDoor.nextRoomIndex);
        // Mark player as just teleported
        player.justTeleported = true;
        StartCoroutine(ResetTeleportFlag(player));
    }
    private IEnumerator ResetTeleportFlag(PlayerHealth player)
    {
        yield return new WaitForSeconds(teleportCooldown);
        player.justTeleported = false;
    }
}