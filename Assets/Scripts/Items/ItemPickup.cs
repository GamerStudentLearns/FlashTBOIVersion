using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerStats stats))
        {
            stats.ModifyStat(item.effect);
            Destroy(gameObject);

        }
    }
}
