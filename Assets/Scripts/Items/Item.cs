using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemID;
    public bool consumable;
    public Sprite itemImage;

    protected Tilemap groundMap;
    public virtual bool UseItem(Vector3Int targetTile)
    {
        Debug.Log($"Using {this.name}\nConsumed: {consumable}");

        return true;
    }

    public virtual void InitItem()
    {
        groundMap = GameManager.instance.groundMap;
    }
}
