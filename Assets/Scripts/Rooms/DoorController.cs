using UnityEngine;
public class DoorController : MonoBehaviour
{
    [Header("Door Parts")]
    public Collider2D doorCollider;
    public SpriteRenderer doorSprite;
    public Sprite openSprite;
    public Sprite closedSprite;
    [Header("Room Transition")]
    public Transform targetSpawn;
    public int nextRoomIndex;
    [HideInInspector] public RoomManager manager;
    [HideInInspector] public RoomController parentRoom;
    void Awake()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        manager = Object.FindFirstObjectByType<RoomManager>();
        parentRoom = GetComponentInParent<RoomController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player hit the door!");
        if (parentRoom != null && !parentRoom.IsCleared())
        {
            Debug.Log("Room not cleared yet!");
            return;
        }
        Debug.Log("Door triggered, moving player");
    }
    public void Open()
    {
        doorCollider.enabled = false;
        doorSprite.sprite = openSprite;
    }
    public void Close()
    {
        doorCollider.enabled = true;
        doorSprite.sprite = closedSprite;
    }
}