using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Seed", order = 1)]
public class Seed : Item
{
    public Plant plant;

    public override void UseItem()
    {
        base.UseItem();
    }
}
