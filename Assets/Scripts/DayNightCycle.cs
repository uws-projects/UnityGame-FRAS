using UnityEngine;
using System;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DayNightCycle : MonoBehaviour {

    public float timeOfTheDay;                  // level starting time in minutes  
    public int sunriseTime = 360;               // 6:00 in the morning in minutes
    private float speed = 0.2f;                  // time multiplier

    public Transform starsTransform;
    public Light Sun;
    public Text timetext;                       // text to display on screen current time

    public Gradient nightDayLightColor;         // pattern for sun color

    public float maxSunIntensity = 1.5f;        // maximum sun brightness
    public float minSunIntensity = 0.0f;        // minimum sun brightness
    public float minPoint = -0.2f;              // offset for the light to live when sun is under the horizon

    public float maxAmbientIntensity = 1.0f;    // maximum ambient light brightness
    public float minAmbientIntensity = 0.0f;    // minium ambient light brigthness
    public float minAmbientPoint = -0.2f;       // offset for the ambient live to live when the sun is under the horizon

    public Gradient nightDayFogColor;           // pattern for fog colour
    public AnimationCurve fogDensityCurve;      // pattern for fog density
    public float fogScale = 1.0f;               // fog scale
    
    public float maxAtmosphereThinckness = 1.2f;// maximum athmosphere thickness visibile at sunrise and sunset
    public float minAtmosphereThickness = 1.4f; // minimum athmosphere thickness visibile at sunrise and sunset

    private float lightVariation;

    Skybox sky;
    Material skyMat;

    private void Start()
    {
        skyMat = RenderSettings.skybox;
        switch (PlayerPrefs.GetInt("TimeOfDay"))
        {
            case 0:
                // morning: 5:00
                timeOfTheDay = 5 * 60 ;
                break;
            case 1:
                // noon: 13:00
                timeOfTheDay = 13 * 60;
                break;
            case 2:
                // evening: 18:00
                timeOfTheDay = 18 * 60;
                break;
            case 3:
                // night: 22:00
                timeOfTheDay = 22 * 60;
                break;
            default:
                timeOfTheDay = 14 * 60;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        updateTimeOfTheDay();
        formatTime();
        updateSky();
        calculateLights();
    }

    private void updateTimeOfTheDay()
    {
        timeOfTheDay = timeOfTheDay + speed * Time.deltaTime;
        /*
            TO DO: remove daymovement on input;
         */
        if (CrossPlatformInputManager.GetButtonDown("RB"))
        {
            speed *= 2.0f;
        }
        if (CrossPlatformInputManager.GetButtonDown("LB"))
        {
            speed /= 2.0f;
        }
        timeOfTheDay = timeOfTheDay > 1440 ? 0 : timeOfTheDay;
    }

    private void formatTime()
    {
        // splitting the time to display it in HH:MM format
        string[] clock = TimeSpan.FromMinutes(timeOfTheDay).ToString().Split(":"[0]);
        timetext.text = clock[0] + ":" + clock[1];
    }

    private void updateSky()
    {
        // rotate sun and stars, apply an offset for the star rotation angle, but make it depend on the sun's rotation angle
        float sunRotation = (timeOfTheDay - sunriseTime) * 0.25f;
        float starsRotation = sunRotation + 280 * 0.25f;

        Sun.transform.rotation = Quaternion.Euler(new Vector3(sunRotation, 0, 0));
        starsTransform.rotation = Quaternion.Euler(new Vector3(starsRotation, 0, 0));
    }

    private float computeIntensity(float min, float max)
    {
        return (max - min) * lightVariation + min;
    }

    private void calculateLights()
    {
        float dotProduct = Vector3.Dot(Sun.transform.forward, Vector3.down);
        lightVariation = Mathf.Clamp01((dotProduct - minPoint) / (1 - minPoint));
        Sun.intensity = computeIntensity(minSunIntensity, maxSunIntensity);

        lightVariation = Mathf.Clamp01(dotProduct - minAmbientPoint) / (1 - minAmbientPoint);
        RenderSettings.ambientIntensity = computeIntensity(minAmbientIntensity, maxAmbientIntensity);

        Sun.color = nightDayLightColor.Evaluate(lightVariation);
        RenderSettings.ambientLight = Sun.color;

        RenderSettings.fogColor = nightDayFogColor.Evaluate(lightVariation);
        RenderSettings.fogDensity = fogDensityCurve.Evaluate(lightVariation) * fogScale;

        skyMat.SetFloat("_AtmosphereThickness", computeIntensity(minAtmosphereThickness, maxAtmosphereThinckness));
    }
}


