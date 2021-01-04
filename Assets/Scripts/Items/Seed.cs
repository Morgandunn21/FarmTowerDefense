using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Seed", order = 1)]
public class Seed : Item
{
    //Tile containing the pplant prefab for this seed
    public GameObject plantPrefab;
    //Tilemap that plants are placed on
    protected Tilemap plantMap;

    //local plant prefab tile
    private GameobjectTile plantPrefabTile;

    //Inits the plant tilemap
    public override void InitItem()
    {
        base.InitItem();

        //Create a Game Object Tile to place on use
        plantMap = GameManager.instance.plantMap;
        plantPrefabTile = CreateInstance<GameobjectTile>();
        plantPrefabTile.tilePrefab = plantPrefab;

        itemImage = plantPrefab.GetComponent<Plant>().seedSprite;
    }

    //Called when the item is used
    public override bool UseItem(Vector3Int targetTile)
    {
        base.UseItem(targetTile);

        //Only plant a seed on dirt and if there is no seed there already
        if(groundMap.GetTile(targetTile).name == "Dirt" && plantMap.GetTile(targetTile) == null)
        {
            //plant the seed
            plantMap.SetTile(targetTile, plantPrefabTile);
            //return successful
            return true;
        }
        else
        {
            //return unsuccessful
            return false;
        }
    }
}
