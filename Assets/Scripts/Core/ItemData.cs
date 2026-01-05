using UnityEngine;

public class ItemData : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public string description;

    public System.Action<PlayerStats> effect;
}
