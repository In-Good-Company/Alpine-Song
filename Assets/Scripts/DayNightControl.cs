using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightControl : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public Material daySky;
    public Material nightSky;
    public float secondsInFullDay = 120f;
    [Tooltip("0 is night, 0.5 is mid day")]
    [Range(0, 1)]
    public float currentTimeOfDay = 0;


    float sunInitialIntensity;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        RenderSettings.skybox = daySky;
    }

    void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay);

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        //Rotate the sun on the X axis by time of day
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;

        //Set sky box material, fog, fog density and intensity of sun and moon by time of day
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {  
            RenderSettings.fog = true;
            RenderSettings.fogDensity = 0.01f;
            intensityMultiplier = 0;
            moon.intensity = 0.175f;
            RenderSettings.skybox = nightSky;
        }
        else if (currentTimeOfDay <= 0.25f)
        {

            RenderSettings.fog = false;
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
            moon.intensity = 0f;
            RenderSettings.skybox = daySky;
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            RenderSettings.fog = false;
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
            moon.intensity = 0f;
            RenderSettings.skybox = daySky;
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
