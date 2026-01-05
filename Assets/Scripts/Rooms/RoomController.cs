using UnityEngine;
using System.Collections.Generic;
public class RoomController : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject rewardPrefab;
    public List<EnemyHealth> enemies = new();
    public DoorController[] doors;
    void Start()
    {
        foreach (var enemy in enemies)
            enemy.OnDeath += CheckRoomClear;
    }
    void CheckRoomClear()
    {
        enemies.RemoveAll(e => e == null);
        if (enemies.Count == 0)
            ClearRoom();
    }
    public void ActivateRoom()
    {
        foreach (var door in doors)
            door.Close();

        enemySpawner.SetActive(true);
    }
    void ClearRoom()
    {
        foreach (var door in doors)
            door.Open();

        Instantiate(rewardPrefab, transform.position, Quaternion.identity);
    }

}