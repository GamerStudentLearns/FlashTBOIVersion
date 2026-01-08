using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;       // Base speed
    public float chaseRadius = 5f;     // Distance at which enemy starts chasing
    [Header("Optional Knockback")]
    public float knockbackForce = 5f;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        // Find the player in the scene when this enemy becomes active
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }
    void FixedUpdate()
    {
        if (player == null) return;
        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;
        if (distance <= chaseRadius)
        {
            moveDirection = toPlayer.normalized;
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
    // Optional: take knockback from player attacks
    public void ApplyKnockback(Vector2 direction)
    {
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
    }
    // Debug: draw chase radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}