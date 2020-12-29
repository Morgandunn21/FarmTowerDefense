using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject InventorySlotParent;
    public GameObject activeItemImage;
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

        StartCoroutine(InitActiveItem());
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

    public void SetActiveItem(int index)
    {
        RectTransform rt = inventorySlots[index].GetComponent<RectTransform>();
        RectTransform activeRT = activeItemImage.GetComponent<RectTransform>();

        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        float width = Vector3.Distance(corners[0], corners[3]);
        float height = Vector3.Distance(corners[0], corners[1]);
        
        activeRT.sizeDelta = new Vector2(width/2, height/2);
        activeRT.anchoredPosition = rt.anchoredPosition;
        activeRT.anchorMax = rt.anchorMax;
        activeRT.anchorMin = rt.anchorMin;
    }

    private IEnumerator InitActiveItem()
    {
        yield return null;

        SetActiveItem(0);
    }
}
