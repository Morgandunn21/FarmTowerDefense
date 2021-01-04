using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Plant : MonoBehaviour
{
    /// <summary>
    /// How many in game hours it takes to fully grow
    /// </summary>
    public int hoursToGrown;
    /// <summary>
    /// How much total health the plant will have
    /// </summary>
    public int health;
    /// <summary>
    /// How many full harvests you van take from the plant
    /// </summary>
    public int numHarvest;

    /// <summary>
    /// sprite of the seed, shown in the item bar
    /// </summary>
    public Sprite seedSprite;
    /// <summary>
    /// sprite of the plant, shown on the tile
    /// </summary>
    public Sprite plantSprite;

    //How much health is removed each time the plant's harvested
    protected int healthPerHarvest;
    //if the plant is fully grown yet
    protected bool grown;
    //how many hours the plant has grown
    protected int totalHoursGrown;
    //how much larger the plant gets each hour while growing
    protected float scalePerHour;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = plantSprite;
        //Start listening for the hour changed event
        TimeManager.OnHourChange += HourChangedHandler;
        //calculate protected variables
        healthPerHarvest = health / numHarvest;
        grown = false;
        totalHoursGrown = 0;
        scalePerHour = 1f / (hoursToGrown + 1);
        //set the initial prefab scale;
        transform.localScale = Vector3.one * scalePerHour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called every ingame hour
    /// </summary>
    /// <param name="daytime">if it is daytime or not</param>
    public virtual void HourChangedHandler(bool daytime)
    {
        //if this plant has not fully grown and it is daytime
        if(!grown && daytime)
        {
            //increment the total hours grown
            totalHoursGrown++;
            //increase the gameobject scale
            transform.localScale += Vector3.one * scalePerHour;
            //if the plant is fully grown
            if(totalHoursGrown == hoursToGrown)
            {
                //change grown to true
                grown = true;
            }
        }
    }
}
