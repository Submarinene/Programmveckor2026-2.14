using UnityEngine;
using UnityEngine.Rendering;

public class LightingController : MonoBehaviour
{
    [Header("Main Light")]
    [SerializeField] private Light mainLight;
    [SerializeField] private Color mainLightColor = Color.white;
    [SerializeField] private float mainLightIntensity = 1.0f;
    
    [Header("Spotlight")]
    [SerializeField] private Light spotlight;
    [SerializeField] private Color spotlightColor = Color.white;
    [SerializeField] private float spotlightIntensity = 2.0f;
    [SerializeField] private float spotlightRange = 10f;
    [SerializeField] private float spotlightAngle = 45f;
    
    [Header("Directional Lights")]
    [SerializeField] private Transform[] directionalLights;
    [SerializeField] private float directionalLightIntensity = 0.5f;
    [SerializeField] private Color directionalLightColor = Color.white;
    
    [Header("Lighting Settings")]
    [SerializeField] private bool enableLightingControls = true;
    [SerializeField] private KeyCode toggleLightKey = KeyCode.L;
    [SerializeField] private KeyCode cycleLightModeKey = KeyCode.K;
    
    private int currentLightMode = 0;
    private string[] lightModeNames = { "Normal", "Spotlight", "Directional", "Dramatic", "Night" };
    
    void Start()
    {
        InitializeLights();
    }
    
    void Update()
    {
        if (!enableLightingControls) return;
        
        HandleLightingInput();
    }
    
    void InitializeLights()
    {
        // Setup main light
        if (mainLight != null)
        {
            mainLight.color = mainLightColor;
            mainLight.intensity = mainLightIntensity;
            mainLight.type = LightType.Point;
        }
        
        // Setup spotlight
        if (spotlight != null)
        {
            spotlight.color = spotlightColor;
            spotlight.intensity = spotlightIntensity;
            spotlight.range = spotlightRange;
            spotlight.spotAngle = spotlightAngle;
            spotlight.type = LightType.Spot;
        }
        
        // Setup directional lights
        foreach (Transform lightTransform in directionalLights)
        {
            if (lightTransform != null)
            {
                Light dirLight = lightTransform.GetComponent<Light>();
                if (dirLight != null)
                {
                    dirLight.color = directionalLightColor;
                    dirLight.intensity = directionalLightIntensity;
                    dirLight.type = LightType.Directional;
                }
            }
        }
    }
    
    void HandleLightingInput()
    {
        // Toggle lights on/off
        if (Input.GetKeyDown(toggleLightKey))
        {
            bool lightsOn = !mainLight.gameObject.activeSelf;
            SetLightsActive(lightsOn);
        }
        
        // Cycle through light modes
        if (Input.GetKeyDown(cycleLightModeKey))
        {
            currentLightMode = (currentLightMode + 1) % lightModeNames.Length;
            ApplyLightMode(currentLightMode);
            Debug.Log($"Light mode: {lightModeNames[currentLightMode]}");
        }
    }
    
    void SetLightsActive(bool active)
    {
        if (mainLight != null)
            mainLight.gameObject.SetActive(active);
            
        if (spotlight != null)
            spotlight.gameObject.SetActive(active);
            
        foreach (Transform lightTransform in directionalLights)
        {
            if (lightTransform != null)
                lightTransform.gameObject.SetActive(active);
        }
    }
    
    void ApplyLightMode(int mode)
    {
        switch (mode)
        {
            case 0: // Normal
                SetNormalLighting();
                break;
            case 1: // Spotlight
                SetSpotlightMode();
                break;
            case 2: // Directional
                SetDirectionalMode();
                break;
            case 3: // Dramatic
                SetDramaticLighting();
                break;
            case 4: // Night
                SetNightLighting();
                break;
        }
    }
    
    void SetNormalLighting()
    {
        if (mainLight != null)
        {
            mainLight.intensity = mainLightIntensity;
            mainLight.color = mainLightColor;
        }
        
        if (spotlight != null)
            spotlight.gameObject.SetActive(false);
            
        foreach (Transform lightTransform in directionalLights)
        {
            if (lightTransform != null)
                lightTransform.gameObject.SetActive(false);
        }
    }
    
    void SetSpotlightMode()
    {
        if (mainLight != null)
        {
            mainLight.intensity = mainLightIntensity * 0.3f;
        }
        
        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(true);
            spotlight.intensity = spotlightIntensity;
        }
        
        foreach (Transform lightTransform in directionalLights)
        {
            if (lightTransform != null)
                lightTransform.gameObject.SetActive(false);
        }
    }
    
    void SetDirectionalMode()
    {
        if (mainLight != null)
        {
            mainLight.intensity = mainLightIntensity * 0.5f;
        }
        
        if (spotlight != null)
            spotlight.gameObject.SetActive(false);
            
        foreach (Transform lightTransform in directionalLights)
        {
            if (lightTransform != null)
            {
                lightTransform.gameObject.SetActive(true);
                Light dirLight = lightTransform.GetComponent<Light>();
                if (dirLight != null)
                {
                    dirLight.intensity = directionalLightIntensity;
                }
            }
        }
    }
    
    void SetDramaticLighting()
    {
        if (mainLight != null)
        {
            mainLight.intensity = mainLightIntensity * 0.2f;
            mainLight.color = Color.Lerp(mainLightColor, Color.red, 0.3f);
        }
        
        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(true);
            spotlight.intensity = spotlightIntensity * 1.5f;
            spotlight.color = Color.Lerp(spotlightColor, Color.red, 0.3f);
        }
        
        foreach (Transform lightTransform in directionalLights)
        {
            if (lightTransform != null)
            lightTransform.gameObject.SetActive(false);
        }
    }
    
    void SetNightLighting()
    {
        if (mainLight != null)
        {
            mainLight.intensity = mainLightIntensity * 0.1f;
            mainLight.color = Color.blue;
        }
        
        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(false);
        }
        
        foreach (Transform lightTransform in directionalLights)
        {
            if (lightTransform != null)
            {
                lightTransform.gameObject.SetActive(false);
            Light dirLight = lightTransform.GetComponent<Light>();
                if (dirLight != null)
                {
                    dirLight.intensity = directionalLightIntensity * 0.3f;
                    dirLight.color = Color.blue * 0.5f;
                }
            }
        }
    }
    
    // Public methods for external control
    public void ToggleLights()
    {
        bool lightsOn = !mainLight.gameObject.activeSelf;
        SetLightsActive(lightsOn);
    }
    
    public void SetLightMode(int mode)
    {
        currentLightMode = Mathf.Clamp(mode, 0, lightModeNames.Length - 1);
        ApplyLightMode(currentLightMode);
    }
    
    public void SetLightIntensity(float intensity)
    {
        if (mainLight != null)
            mainLight.intensity = intensity;
            
        if (spotlight != null)
            spotlight.intensity = intensity;
            
        foreach (Transform lightTransform in directionalLights)
        {
            Light dirLight = lightTransform.GetComponent<Light>();
            if (dirLight != null)
                dirLight.intensity = intensity * 0.5f;
        }
    }
}
