using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

    public String startHour;                    // HH:mm format
    public float timeOfTheDay;                  // level starting time in minutes  
    public int sunriseTime = 360;               // 6:00 in the morning in minutes
    public float speed = 1.0f;                  // time multiplier

    public Transform starsTransform;
    public Light Sun;
    public Text timetext;                       // text to display on screen current time

    public Gradient nightDayLightColor;         // pattern for sun color

    public float maxSunIntensity = 3.0f;        // maximum sun brightness (no HDR so 3 is enough)   
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
        if (startHour != null)
        {
            string[] time = startHour.Split(':');
            int hours, minutes;
            int.TryParse(time[0], out hours);
            int.TryParse(time[1], out minutes);
            timeOfTheDay = hours * 60 + minutes;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            speed += 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            speed -= 1.0f;
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


