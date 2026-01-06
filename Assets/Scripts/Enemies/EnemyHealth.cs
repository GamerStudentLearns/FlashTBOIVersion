using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 5f;
    private float currentHealth;

    public event System.Action OnDeath;

    void Awake()
    {
               currentHealth = maxHealth;

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        HitStopController.instance.Stop(0.03f);
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
