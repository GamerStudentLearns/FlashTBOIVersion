using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private RoomController room;
    private bool activated;

    void Awake()
    {
        room = GetComponentInParent<RoomController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (other.CompareTag("Player"))
        {
            activated = true;
            room.ActivateRoom();
        }
    }
}
