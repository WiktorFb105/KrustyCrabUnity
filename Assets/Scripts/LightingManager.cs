using UnityEngine;

[ExecuteAlways]
public class LightingScript : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] private float DayDuration = 60f;

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += (24f / DayDuration) * Time.deltaTime;

            if (TimeOfDay >= 24f)
                TimeOfDay = 0f;
        }

        UpdateLighting(TimeOfDay / 24f);
    }

    private void UpdateLighting(float timePercent)
    {
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation =
                Quaternion.Euler(
                    (timePercent * 360f) - 90f,
                    170f,
                    0f
                );
        }

        RenderSettings.ambientLight =
            Preset.AmbientColor.Evaluate(timePercent);

        RenderSettings.fogColor =
            Preset.FogColor.Evaluate(timePercent);
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsByType<Light>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    break;
                }
            }
        }
    }
}