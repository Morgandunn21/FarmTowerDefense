using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
