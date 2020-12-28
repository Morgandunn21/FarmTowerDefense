using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public uint numItems;

    private Item[] items;
    private uint[] itemCounts;

    // Start is called before the first frame update
    void Start()
    {
        items = new Item[numItems];
        itemCounts = new uint[numItems];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Handles adding a given item to the inventory if possible
    /// </summary>
    /// <param name="c">item to add</param>
    /// <returns>if the item was added successfully</returns>
    public bool PickupItem(Item c)
    {
        bool success = false;
        int emptyIndex = -1;

        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                if(emptyIndex < 0)
                {
                    emptyIndex = i;
                }
            }
            else if(c.itemID == items[i].itemID)
            {
                success = true;
                itemCounts[i]++;
                break;
            }
        }

        if(success == false && emptyIndex >= 0)
        {
            items[emptyIndex] = c;
            itemCounts[emptyIndex] = 1;
        }

        return success;
    }

    public void UseItem(uint index)
    {
        Item item = items[index];

        if(item != null)
        {
            item.UseItem();

            if(item.consumable)
            {
                itemCounts[index]--;
            }
        }
    }
}
