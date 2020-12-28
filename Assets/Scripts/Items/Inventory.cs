using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public uint numItems;

    private Item[] items;
    private uint[] itemCounts;

    private InventoryUI inventoryUI;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = GameManager.instance.inventoryUI;
        inventoryUI.InitInventory(numItems);
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
        bool itemFound = false;
        int emptyIndex = -1;

        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                if(emptyIndex < 0)
                {
                    emptyIndex = i;

                    if(itemFound)
                    {
                        break;
                    }
                }
            }
            else if(!itemFound && c.itemID == items[i].itemID)
            {
                itemFound = true;

                if(c.consumable)
                {
                    success = true;
                    itemCounts[i]++;
                    inventoryUI.SetCount(i, itemCounts[i].ToString());
                    break;
                }
                else if(emptyIndex >= 0)
                {
                    break;
                }
            }
        }

        if(success == false && emptyIndex >= 0)
        {
            items[emptyIndex] = c;
            inventoryUI.SetItem(emptyIndex, c.itemImage);

            if(c.consumable)
            {
                itemCounts[emptyIndex] = 1;
                inventoryUI.SetCount(emptyIndex, "1");
            }
            else
            {
                inventoryUI.SetCount(emptyIndex, "");
            }

            success = true;
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
