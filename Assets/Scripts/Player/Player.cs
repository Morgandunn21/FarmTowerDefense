using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public uint numItems;
    public SpriteRenderer heldItem;

    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory(numItems, this);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseScroll = Input.mouseScrollDelta.y;

        if(mouseScroll != 0)
        {
            inventory.ChangeActive(mouseScroll);
        }

        if(Input.GetMouseButtonDown(0))
        {
            inventory.UseItem();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Pickup pickup;
        if(collider.gameObject.TryGetComponent<Pickup>(out pickup))
        {
            if (inventory.PickupItem(pickup.item))
            {
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

    public void SetHeldItem(Sprite item)
    {
        heldItem.sprite = item;
    }
}
