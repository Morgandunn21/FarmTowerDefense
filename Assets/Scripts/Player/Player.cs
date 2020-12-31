using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Number of inventory slots
    /// </summary>
    public uint numItems;
    /// <summary>
    /// Sprite Renderer for the held item
    /// </summary>
    public SpriteRenderer heldItem;
    /// <summary>
    /// GameObject for the hovered tile image
    /// </summary>
    public GameObject hoveredTile;
    public GameObject playerReach;
    /// <summary>
    /// How far away the player can select a tile
    /// </summary>
    public float range;

    //The player's inventory
    private Inventory inventory;
    //The coordinates of the highlighted tile
    private Vector3Int highlightedSquare;
    //Reference to the tilemap holding ground tiles
    private Tilemap groundMap;
    //reference to the main camera
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize private variables/references
        inventory = new Inventory(numItems, this);
        groundMap = GameManager.instance.groundMap;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Get any input regarding the inventory
        GetInventoryInput();
        //Get the current square highlighted by the player
        FindHighlightedSquare();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("Farm TD Screenshot");
        }
    }

    /// <summary>
    /// Called when the player's collider hits a trigger
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        //variable to store the pickup of this collider
        Pickup pickup;
        //If the collider had a pickup
        if(collider.gameObject.TryGetComponent<Pickup>(out pickup))
        {
            //If the pickup was successfully added to the inventory
            if (inventory.PickupItem(pickup.item))
            {
                //destroy the pickup on the ground
                Destroy(collider.gameObject);
            }
        }
    }

    /// <summary>
    /// Respawns the player at checkpoint.
    /// </summary>
    public void Respawn()
    {
    }

    /// <summary>
    /// Changes the sprite of the held item
    /// </summary>
    /// <param name="item"></param>
    public void SetHeldItem(Sprite item)
    {
        heldItem.sprite = item;
    }

    /// <summary>
    /// Calculates what square the player is currently highlighting
    /// </summary>
    private void FindHighlightedSquare()
    {
        //Turn the mouse position to world coordinates
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //make sure the mouse position z is 1
        mousePos.z = 1;
        //find the distance from the player to the mouse
        var mouseDir = mousePos - transform.position;

        //if the mouse is farther than the player's range, normalize the distance
        if(mouseDir.magnitude > range)
        {
            mouseDir.Normalize();
            mouseDir *= range;
        }

        playerReach.transform.position = transform.position + mouseDir;
        //get what square the player is selecting 
        highlightedSquare = groundMap.WorldToCell(transform.position + mouseDir);
        //move the highlighted square image to that position
        hoveredTile.transform.position = groundMap.CellToWorld(highlightedSquare) + new Vector3(0.5f, 0.5f, 0);
    }

    public Vector3Int GetHighightedSquare()
    {
        return highlightedSquare;
    }

    private void GetInventoryInput()
    {
        float mouseScroll = Input.mouseScrollDelta.y;


        if (mouseScroll != 0)
        {
            inventory.ChangeActive(mouseScroll);
        }

        if (Input.GetMouseButtonDown(0))
        {
            inventory.UseItem();
        }
    }
}
