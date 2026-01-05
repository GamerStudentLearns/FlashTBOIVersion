using UnityEngine;
using System.Collections.Generic;
public class RoomController : MonoBehaviour
{
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
    void ClearRoom()
    {
        foreach (var door in doors)
            door.Open();
    }
}