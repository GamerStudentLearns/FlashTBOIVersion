using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class RoomPrefab
{
    public GameObject prefab;
    public bool doorTop;
    public bool doorBottom;
    public bool doorLeft;
    public bool doorRight;
}
public class ProceduralDungeonGenerator : MonoBehaviour
{
    public static ProceduralDungeonGenerator Instance;
    [Header("Room Prefabs")]
    public List<RoomPrefab> roomPrefabs;
    [Header("Generation Settings")]
    public int totalRooms = 10;
    public Vector2Int startPos = Vector2Int.zero;
    public Vector2 roomSpacing = new Vector2(20f, 12f);
    [Header("Player")]
    public GameObject playerPrefab;
    [Header("Camera")]
    public RoomCameraController cameraController;
    private Dictionary<Vector2Int, RoomController> spawnedRooms = new();
    private RoomController currentRoom;
    private GameObject player;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GenerateDungeon();
    }
    void GenerateDungeon()
    {
        Queue<Vector2Int> toProcess = new();
        toProcess.Enqueue(startPos);
        spawnedRooms.Clear();
        int roomsCreated = 0;
        while (roomsCreated < totalRooms && toProcess.Count > 0)
        {
            Vector2Int pos = toProcess.Dequeue();
            if (spawnedRooms.ContainsKey(pos)) continue;
            bool doorTop = spawnedRooms.ContainsKey(pos + Vector2Int.up);
            bool doorBottom = spawnedRooms.ContainsKey(pos + Vector2Int.down);
            bool doorLeft = spawnedRooms.ContainsKey(pos + Vector2Int.left);
            bool doorRight = spawnedRooms.ContainsKey(pos + Vector2Int.right);
            GameObject prefab = PickCompatiblePrefab(doorTop, doorBottom, doorLeft, doorRight);
            if (prefab == null) continue;
            GameObject roomObj = Instantiate(
                prefab,
                new Vector3(pos.x * roomSpacing.x, pos.y * roomSpacing.y, 0),
                Quaternion.identity
            );
            RoomController roomController = roomObj.GetComponent<RoomController>();
            spawnedRooms.Add(pos, roomController);
            roomsCreated++;
            foreach (Vector2Int dir in new[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left })
            {
                Vector2Int next = pos + dir;
                if (!spawnedRooms.ContainsKey(next) && !toProcess.Contains(next))
                {
                    if (Random.value < 0.6f)
                        toProcess.Enqueue(next);
                }
            }
            roomObj.SetActive(false);
        }
        // Activate starting room
        if (!spawnedRooms.ContainsKey(startPos))
        {
            Debug.LogError("No starting room generated!");
            return;
        }
        currentRoom = spawnedRooms[startPos];
        currentRoom.gameObject.SetActive(true);
        currentRoom.ActivateRoom();
        SpawnPlayer(currentRoom);
        // Snap camera to starting room
        if (cameraController != null)
            cameraController.SnapToTarget(currentRoom.transform);
    }
    void SpawnPlayer(RoomController room)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab not assigned!");
            return;
        }
        Transform spawnPoint = room.transform.Find("SpawnPoint");
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : room.transform.position;
        player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        player.transform.localScale = Vector3.one;
    }
    GameObject PickCompatiblePrefab(bool top, bool bottom, bool left, bool right)
    {
        List<GameObject> candidates = new();
        foreach (RoomPrefab rp in roomPrefabs)
        {
            if ((top == rp.doorTop || !top) &&
                (bottom == rp.doorBottom || !bottom) &&
                (left == rp.doorLeft || !left) &&
                (right == rp.doorRight || !right))
            {
                candidates.Add(rp.prefab);
            }
        }
        return candidates.Count > 0
            ? candidates[Random.Range(0, candidates.Count)]
            : null;
    }
    // Called by DoorTrigger
    public void EnterRoom(RoomController fromRoom, DoorDirection direction)
    {
        Vector2Int fromPos = GetRoomGridPosition(fromRoom);
        Vector2Int offset = direction switch
        {
            DoorDirection.Top => Vector2Int.up,
            DoorDirection.Bottom => Vector2Int.down,
            DoorDirection.Left => Vector2Int.left,
            DoorDirection.Right => Vector2Int.right,
            _ => Vector2Int.zero
        };
        Vector2Int targetPos = fromPos + offset;
        if (!spawnedRooms.ContainsKey(targetPos)) return;
        // Deactivate old room
        fromRoom.gameObject.SetActive(false);
        // Activate new room
        currentRoom = spawnedRooms[targetPos];
        currentRoom.gameObject.SetActive(true);
        currentRoom.ActivateRoom();
        // Move player
        Transform spawn = currentRoom.transform.Find("SpawnPoint");
        if (spawn != null)
            player.transform.position = spawn.position;
        // Move camera
        if (cameraController != null)
            cameraController.FocusRoom(currentRoom.transform);
    }
    Vector2Int GetRoomGridPosition(RoomController room)
    {
        foreach (var kvp in spawnedRooms)
            if (kvp.Value == room)
                return kvp.Key;
        Debug.LogError("Room position not found!");
        return Vector2Int.zero;
    }
}