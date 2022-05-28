using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material dayMat;
    public Material nightMat;
    public GameObject dayLight;
    public GameObject nightLight;
    public Color dayFog;
    public Color nightFog;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Roataion", Time.time * 0.5f);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(5, 40, 80, 20), "Day"))
        {
            RenderSettings.skybox = dayMat;
            RenderSettings.fogColor = dayFog;
            dayLight.SetActive(true);
            nightLight.SetActive(false);
        }

        if (GUI.Button(new Rect(5, 70, 80, 20), "Night"))
        {
            RenderSettings.skybox = nightMat;
            RenderSettings.fogColor = nightFog;
            dayLight.SetActive(false);
            nightLight.SetActive(true);
        }
    }
}
