using UnityEngine;
public class RoomManager : MonoBehaviour
{
    [Header("Rooms")]
    public RoomController[] rooms;                 // Assign all room prefabs or scene instances
    public RoomCameraController cameraController;  // Camera script to follow active room
    [HideInInspector] public RoomController currentRoom; // Currently active room
    void Start()
    {
        if (rooms == null || rooms.Length == 0)
        {
            Debug.LogError("No rooms assigned in RoomManager!");
            return;
        }
        // Activate the first room at start
        ActivateRoom(0);
    }
    /// <summary>
    /// Activates the room at the given index
    /// </summary>
    /// <param name="index">Index of the room in the rooms array</param>
    public void ActivateRoom(int index)
    {
        if (index < 0 || index >= rooms.Length)
        {
            Debug.LogError("Room index out of range: " + index);
            return;
        }
        // Deactivate previous room
        if (currentRoom != null)
            currentRoom.gameObject.SetActive(false);
        // Activate the new room
        currentRoom = rooms[index];
        currentRoom.gameObject.SetActive(true);
        // Call room's ActivateRoom (closes doors, activates enemies)
        currentRoom.ActivateRoom();
        // Move player to the SpawnPoint of this room
        Transform spawn = currentRoom.transform.Find("SpawnPoint");
        if (spawn != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                player.transform.position = spawn.position;
        }
        // Focus camera on the new room
        if (cameraController != null)
            cameraController.FocusRoom(currentRoom.transform);
        Debug.Log("Activated room: " + currentRoom.name);
    }
}