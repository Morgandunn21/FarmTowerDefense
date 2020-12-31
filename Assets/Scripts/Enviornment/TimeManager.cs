using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    [Header("Day Length Properties")]
    /// <summary>
    /// How many seconds the entire day lasts
    /// </summary>
    public float lengthOfDay;
    /// <summary>
    /// What hour the night transitions to day
    /// </summary>
    public float dawnStart;
    /// <summary>
    /// what hour the day transitions to night
    /// </summary>
    public float duskStart;
    /// <summary>
    /// how long in hours is the transition from night to day
    /// </summary>
    public float transitionLength;

    [Header("Sunlight Properties")]
    /// <summary>
    /// Color of the sun during the day
    /// </summary>
    public Color dayLight;
    /// <summary>
    /// Color of the sun at sunrise/sunset
    /// </summary>
    public Color twilight;
    /// <summary>
    /// color of the sun at night
    /// </summary>
    public Color nightLight;
    /// <summary>
    /// The ambient light representing the sun
    /// </summary>
    public Light2D sunLight;

    //lenght in seconds of one in game hour
    private float lengthOfHour;
    //the current in game hour
    private int currentHour;
    //the number of seconds that have passed since this hour started
    private float currentTime;
    //whether the time is in the day or night
    private bool daytime;
    [SerializeField]
    //the color the sun changes to throughout the day
    private Gradient sunColor;
    private bool sunUpdateFlag;

    // Start is called before the first frame update
    void Awake()
    {
        lengthOfHour = lengthOfDay / 24;
        currentHour = (int)dawnStart;
        currentTime = 0;
        daytime = true;
        sunUpdateFlag = true;
        InitSunColors();
        Debug.Log($"Length of Day: {lengthOfDay}");
        Debug.Log($"Length of Hour: {lengthOfHour}");
        Debug.Log($"Current Hour: {currentHour}");
        Debug.Log($"Current Time: {currentTime}");
        Debug.Log($"-------------------------------");
    }

    // Update is called once per frame
    void Update()
    {
        //increment the time since the hour started
        currentTime += Time.deltaTime;

        //if the hour is over
        if (currentTime >= lengthOfHour)
        {
            //increment the current hour
            currentHour++;

            //if midnight, roll over to hour 0
            if(currentHour >= 24)
            {
                currentHour = 0;
            }
            //if start of day, change daytime to true
            else if(currentHour == dawnStart)
            {
                daytime = true;
            }
            //if end of day, change daytime to false
            else if(currentHour == duskStart)
            {
                daytime = false;
            }

            //reset current time to 0
            currentTime = 0;
            UpdateSun(0);
            //reset the update flag to true
            sunUpdateFlag = true;
        }
        //else if we are in the last quarter hour
        else if(currentTime >= lengthOfHour * 3/4)
        {
            //if the update flag is true
            if(sunUpdateFlag)
            {
                //update the sun to the 3rd quarter hour
                UpdateSun(3);
                //set the update flag to false
                sunUpdateFlag = false;
            }
        }
        //else if we are in the 3rd quarter hour
        else if (currentTime >= lengthOfHour * 2 / 4)
        {
            //if the update flag is false
            if(!sunUpdateFlag)
            {
                //update the sun for the 2nd quarter hour
                UpdateSun(2);
                //set the flag to true
                sunUpdateFlag = true;
            }
        }
        //else if we are in the 2nd quarter hour
        else if (currentTime >= lengthOfHour * 1 / 4)
        {
            //if the flag is true
            if(sunUpdateFlag)
            {
                //update the sun for the first quarter hour
                UpdateSun(1);
                //set the flag to false
                sunUpdateFlag = false;
            }
        }
    }

    private void UpdateSun(int quarterHour)
    {
        Debug.Log($"Update Sun: {((float)currentHour + (quarterHour / 4f)) / 24}");
        sunLight.color = sunColor.Evaluate(((float)currentHour + (quarterHour / 4f)) / 24);
    }

    private void InitSunColors()
    {
        sunColor = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[8];
        //Color at midnight
        colorKeys[0] = colorKey(nightLight, 0.0f);
        //transition to day colors
        colorKeys[1] = colorKey(nightLight, (dawnStart - transitionLength / 2) / 24);
        colorKeys[2] = colorKey(twilight, dawnStart / 24);
        colorKeys[3] = colorKey(dayLight, (dawnStart + transitionLength/2) / 24);
        //transition to night colors
        colorKeys[4] = colorKey(dayLight, (duskStart - transitionLength / 2) / 24);
        colorKeys[5] = colorKey(twilight, duskStart / 24);
        colorKeys[6] = colorKey(nightLight, (duskStart + transitionLength / 2) / 24);
        //color at midnight
        colorKeys[7] = colorKey(nightLight, 0.0f);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

        sunColor.SetKeys(colorKeys, alphaKeys);

        UpdateSun(0);
    }

    private GradientColorKey colorKey(Color color, float time)
    {
        return new GradientColorKey(color, time);
    }
}
