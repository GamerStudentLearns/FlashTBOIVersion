using UnityEngine;
using System;

public abstract class ItemData : ScriptableObject
{

    public string itemName;
    [TextArea]
    public string description;


    protected Action<PlayerStats> effect;

    public virtual void Apply(PlayerStats stats)
    {
        stats.ModifyStat(effect);
        ItemPopupUI.Instance?.Show(itemName, description);
    }
}
