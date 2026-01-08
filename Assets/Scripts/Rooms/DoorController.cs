using UnityEngine;
public enum DoorDirection { Top, Bottom, Left, Right }
public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public DoorDirection direction;
    public RoomController parentRoom;
    [Header("Visuals")]
    public Collider2D doorCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closedSprite;
    public void Open()
    {
        if (doorCollider != null) doorCollider.enabled = false;
        if (spriteRenderer != null && openSprite != null) spriteRenderer.sprite = openSprite;
    }
    public void Close()
    {
        if (doorCollider != null) doorCollider.enabled = true;
        if (spriteRenderer != null && closedSprite != null) spriteRenderer.sprite = closedSprite;
    }
}