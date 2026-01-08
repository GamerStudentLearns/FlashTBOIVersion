using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Room Setup")]
    public GameObject enemySpawner;   // Parent of all enemies in this room
    public DoorController[] doors;    // Doors to control
    public GameObject rewardPrefab;   // Reward to spawn

    private bool roomCleared = false;
    private bool roomActive = false;

    public void ActivateRoom()
    {
        roomActive = true;
        roomCleared = false;

        // Close all doors
        foreach (var door in doors)
            door.Close();

        // Activate enemies
        enemySpawner.SetActive(true);
    }

    void Update()
    {
        // Don't check until the room is active
        if (!roomActive || roomCleared)
            return;

        // Check if any enemies are still alive
        foreach (EnemyHealth e in enemySpawner.GetComponentsInChildren<EnemyHealth>(true))
        {
            if (e != null && e.gameObject.activeInHierarchy)
                return; // At least one enemy still alive
        }

        // No enemies left
        ClearRoom();
    }

    private void ClearRoom()
    {
        roomCleared = true;
        roomActive = false;

        // Open doors
        foreach (var door in doors)
            door.Open();

        // Spawn reward once
        if (rewardPrefab != null)
            Instantiate(rewardPrefab, transform.position, Quaternion.identity);

        Debug.Log("Room cleared! Doors open!");
    }

    public bool IsCleared()
    {
        return roomCleared;
    }
}
