using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemID;
    public bool consumable;
    public Sprite itemImage;

    public virtual void UseItem()
    {

    }
}
