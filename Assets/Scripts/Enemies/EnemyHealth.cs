using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tear"))
        {
            Destroy(other.gameObject);
            health--;

            if (health <= 0)
                {
                Destroy(gameObject);
            }
        }
    }
}
