using UnityEngine;
public class EnemyWanderShooter : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float wanderRadius = 3f;
    public float wanderInterval = 2f;
    public float idleThreshold = 0.05f;
    [Header("Combat")]
    public float detectionRange = 6f;
    public float fireRate = 1.2f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    // Walking sprites
    public Sprite walkUp;
    public Sprite walkDown;
    public Sprite walkLeft;
    public Sprite walkRight;
    // Shooting sprites
    public Sprite shootUp;
    public Sprite shootDown;
    public Sprite shootLeft;
    public Sprite shootRight;
    // Idle sprites
    public Sprite idleUp;
    public Sprite idleDown;
    public Sprite idleLeft;
    public Sprite idleRight;
    private Transform player;
    private Vector2 wanderTarget;
    private float wanderTimer;
    private float fireTimer;
    private Vector2 lastMoveDirection = Vector2.down;
    private Vector2 velocity;
    private bool isShooting;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewWanderTarget();
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isShooting = distanceToPlayer <= detectionRange;
        if (isShooting)
        {
            velocity = Vector2.zero;
            AttackPlayer();
            UpdateShootingSprite();
        }
        else
        {
            Wander();
            UpdateMovementOrIdleSprite();
        }
    }
    // --------------------
    // WANDERING
    // --------------------
    void Wander()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0)
            PickNewWanderTarget();
        MoveTowards(wanderTarget);
    }
    void PickNewWanderTarget()
    {
        wanderTarget = (Vector2)transform.position +
                       Random.insideUnitCircle * wanderRadius;
        wanderTimer = wanderInterval;
    }
    // --------------------
    // ATTACKING
    // --------------------
    void AttackPlayer()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireRate;
        }
    }
    void Shoot()
    {
        if (!projectilePrefab || !firePoint) return;
        GameObject proj = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );
        Vector2 dir = (player.position - firePoint.position).normalized;
        proj.GetComponent<projectile>().SetDirection(dir);
    }
    // --------------------
    // MOVEMENT
    // --------------------
    void MoveTowards(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position);
        velocity = dir.normalized * moveSpeed;
        if (velocity.magnitude > idleThreshold)
            lastMoveDirection = velocity.normalized;
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }
    // --------------------
    // SPRITES
    // --------------------
    void UpdateMovementOrIdleSprite()
    {
        if (velocity.magnitude <= idleThreshold)
            UpdateIdleSprite();
        else
            UpdateWalkSprite();
    }
    void UpdateWalkSprite()
    {
        Vector2 dir = lastMoveDirection;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            spriteRenderer.sprite = dir.x > 0 ? walkRight : walkLeft;
        else
            spriteRenderer.sprite = dir.y > 0 ? walkUp : walkDown;
    }
    void UpdateIdleSprite()
    {
        Vector2 dir = lastMoveDirection;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            spriteRenderer.sprite = dir.x > 0 ? idleRight : idleLeft;
        else
            spriteRenderer.sprite = dir.y > 0 ? idleUp : idleDown;
    }
    void UpdateShootingSprite()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            spriteRenderer.sprite = dir.x > 0 ? shootRight : shootLeft;
        else
            spriteRenderer.sprite = dir.y > 0 ? shootUp : shootDown;
    }
}