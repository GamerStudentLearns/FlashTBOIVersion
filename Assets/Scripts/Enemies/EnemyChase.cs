using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

    }
}
