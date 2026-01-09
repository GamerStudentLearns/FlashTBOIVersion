using UnityEngine;

[CreateAssetMenu(menuName = "Items/Small Rock")]
public class SmallRock : ItemData
{
    private void OnEnable()
    {
        effect = stats =>
        {
            stats.damage += 1f;
            stats.moveSpeed -= 1f;
        };
    }
}

