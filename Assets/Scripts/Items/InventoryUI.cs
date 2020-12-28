using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject InventorySlotParent;
    public GameObject InventorySlotPrefab;

    private GameObject[] inventorySlots;

    public void InitInventory(uint numSlots)
    {
        inventorySlots = new GameObject[numSlots];

        for(int i = 0; i < numSlots; i++)
        {
            inventorySlots[i] = Instantiate(InventorySlotPrefab, InventorySlotParent.transform);
            inventorySlots[i].GetComponent<InventorySlotUI>().itemImage.enabled = false;
        }
    }

    public void SetItem(int index, Sprite image)
    {
        InventorySlotUI slot = inventorySlots[index].GetComponent<InventorySlotUI>();

        if(image != null)
        {
            slot.itemImage.enabled = true;
        }
        else
        {
            slot.itemImage.enabled = false;
        }

        slot.itemImage.sprite = image;
    }

    public void SetCount(int index, string count)
    {
        InventorySlotUI slot = inventorySlots[index].GetComponent<InventorySlotUI>();

        slot.itemCount.text = count;
    }
}
