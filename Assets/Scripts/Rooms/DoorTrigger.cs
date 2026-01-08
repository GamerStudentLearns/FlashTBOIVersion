using UnityEngine;
public class DoorTrigger : MonoBehaviour
{
    public DoorController door;
    private bool used = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;
        if (door == null || door.parentRoom == null) return;
        if (!door.parentRoom.IsCleared()) return;
        used = true;
        // Ask the generator to enter the next room
        ProceduralDungeonGenerator.Instance.EnterRoom(door.parentRoom, door.direction);
    }
}