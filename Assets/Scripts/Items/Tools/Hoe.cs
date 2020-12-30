using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Tools/Hoe", order = 1)]
public class Hoe : Item
{
    public GameobjectTile dirtTile;

    public override bool UseItem(Vector3Int targetTile)
    {
        base.UseItem(targetTile);

        groundMap.SetTile(targetTile, dirtTile);

        return true;
    }
}
