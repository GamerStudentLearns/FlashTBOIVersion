using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Parts")]
    public Collider2D doorCollider;
    public SpriteRenderer doorSprite;

    public Sprite openSprite;
    public Sprite closedSprite;

    public Transform targetSpawn;

    void Awake()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        transform.localScale = Vector3.one;

        if (doorSprite != null)
            doorSprite.transform.localScale = Vector3.one;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        other.transform.position = targetSpawn.position;
    }
}
