using UnityEngine;
public class RoomController : MonoBehaviour
{
    [Header("Room Setup")]
    public GameObject enemySpawner;       // Parent of all enemies in this room
    public DoorController[] doors;        // Doors to control
    public GameObject rewardPrefab;       // Reward to spawn
    private bool roomCleared = false;     // Prevents repeated clearing
                                          // Call this to start the room
    public void ActivateRoom()
    {
        // Close all doors
        foreach (var door in doors)
            door.Close();
        // Activate the enemy spawner
        enemySpawner.SetActive(true);
        roomCleared = false;
    }
    void Update()
    {
        // If already cleared, do nothing
        if (roomCleared) return;
        // Check if any active enemies exist
        bool anyAlive = false;
        foreach (EnemyHealth e in enemySpawner.GetComponentsInChildren<EnemyHealth>(true))
        {
            if (e != null && e.gameObject.activeInHierarchy)
            {
                anyAlive = true;
                break;
            }
        }
        // If none alive, clear room
        if (!anyAlive)
            ClearRoom();
    }
    private void ClearRoom()
    {
        roomCleared = true;
        // Open doors
        foreach (var door in doors)
            door.Open();
        // Spawn reward
        if (rewardPrefab != null)
            Instantiate(rewardPrefab, transform.position, Quaternion.identity);
        Debug.Log("Room cleared! Doors open!");
    }
}