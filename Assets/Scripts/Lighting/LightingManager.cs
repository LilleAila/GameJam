using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay = 12;
    public static float StaticTime = 12;
    public static bool night = false;
    [SerializeField, Range(0, 1)] private float TimeSpeed = 1;

    [SerializeField, Range(0, 24)] public float DayStart = 8;
    [SerializeField, Range(0, 24)] public float NightStart = 21;

    private void Update()
    {
        if (Preset == null) return;

        if(Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * TimeSpeed;
            TimeOfDay %= 24;
        }
        UpdateLighting(TimeOfDay / 24f);
        StaticTime = TimeOfDay;
        if(TimeOfDay >= NightStart || TimeOfDay <= DayStart)
        {
            night = true;
        } else
        {
            night = false;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.AmbientColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0f));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null) return;

        if (RenderSettings.sun != null) DirectionalLight = RenderSettings.sun;
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
