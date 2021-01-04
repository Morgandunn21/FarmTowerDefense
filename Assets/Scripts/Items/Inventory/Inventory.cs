using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private uint numItems;

    private Item[] items;
    private uint[] itemCounts;

    private InventoryUI inventoryUI;
    private int activeItem = 0;
    private Player player;

    public Inventory(uint ni, Player p)
    {
        numItems = ni;
        player = p;
        inventoryUI = GameManager.instance.inventoryUI;
        inventoryUI.InitInventory(numItems);
        items = new Item[numItems];
        itemCounts = new uint[numItems];
    }

    public void ChangeActive(float mouseScroll)
    {
        if (mouseScroll > 0)
        {
            if (activeItem < numItems - 1)
            {
                activeItem++;
                inventoryUI.SetActiveItem(activeItem);
                player.SetHeldItem(items[activeItem]?.itemImage);
            }
        }
        else if (mouseScroll < 0)
        {
            if (activeItem > 0)
            {
                activeItem--;
                inventoryUI.SetActiveItem(activeItem);
                player.SetHeldItem(items[activeItem]?.itemImage);
            }
        }
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

            if(emptyIndex == activeItem)
            {
                player.SetHeldItem(c.itemImage);
            }

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

            c.InitItem();

            success = true;
        }

        return success;
    }

    public void UseItem(int index = -1)
    {
        if(index == -1)
        {
            index = activeItem;
        }

        Item item = items[index];

        if(item != null)
        {
            bool success = item.UseItem(player.GetHighightedSquare());

            if(item.consumable && success)
            {
                uint count = --itemCounts[index];

                if(count == 0)
                {
                    inventoryUI.SetItem(index, null);
                    inventoryUI.SetCount(index, "");
                    player.SetHeldItem(null);
                    items[index] = null;
                }
                else
                {
                    inventoryUI.SetCount(index, count.ToString());
                }
            }
        }
    }
}
