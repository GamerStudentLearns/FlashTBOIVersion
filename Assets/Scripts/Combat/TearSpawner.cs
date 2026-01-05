using UnityEngine;

public class TearSpawner : MonoBehaviour
{
    public GameObject tearPrefab;
    private PlayerStats playerStats;
    private float cooldown;

    void Awake()
    {
               playerStats = GetComponent<PlayerStats>();

    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        Vector2 dir = GetShootDirection();
        if (dir != Vector2.zero && cooldown <= 0)
        {
            SpawnTear(dir);
            cooldown = playerStats.fireRate;
        }
    }

    void SpawnTear(Vector2 dir)
    {
        var tear = Instantiate(tearPrefab, transform.position, Quaternion.identity).GetComponent<Tear>();

        tear.damage = playerStats.damage;
        tear.speed = playerStats.shotSpeed;
        tear.range = playerStats.range;

        tear.GetComponent<Rigidbody2D>().linearVelocity = dir * playerStats.shotSpeed;
    }

    Vector2 GetShootDirection()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow)) direction = Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow)) direction = Vector2.down;
        if (Input.GetKey(KeyCode.LeftArrow)) direction = Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow)) direction = Vector2.right;
        return direction;
    }
}
