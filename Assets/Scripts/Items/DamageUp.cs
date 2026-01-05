using UnityEngine;

public class DamageUp : ItemData
{
    private void OnEnable()
    {
        effect = stats => stats.damage += 1.5f;
    }
}