using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D/Tiles/Game Object Tile", order = 1)]
public class GameobjectTile : TileBase
{
    public Sprite sprite = null;
    public GameObject tilePrefab;
    public UnityEngine.Tilemaps.Tile.ColliderType colliderType;

    /// <summary>
    /// Retrieves any tile rendering data from the scripted tile.
    /// </summary>
    /// <param name="position">Position of the Tile on the Tilemap.</param>
    /// <param name="tilemap">The Tilemap the tile is present on.</param>
    /// <param name="tileData">Data to render the tile.</param>
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        var iden = Matrix4x4.identity;

        tileData.sprite = sprite;
        tileData.gameObject = tilePrefab;
        tileData.colliderType = colliderType;
        tileData.flags = TileFlags.LockTransform;
        tileData.transform = iden;
    }
}
