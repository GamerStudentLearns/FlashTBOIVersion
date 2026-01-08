using UnityEngine;
public class HeartPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float floatAmplitude = 0.25f;   // how high it floats
    public float floatSpeed = 2f;          // how fast it floats
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        // Float effect (up/down)
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only affect the player
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.Heal(1);   // Heal exactly 1 heart
                Destroy(gameObject);
            }
        }
    }
}