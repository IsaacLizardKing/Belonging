using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SceneTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Light2D LightComponent;
    
    public float slerpSpeed;
    float curSlurp;
    public float targetBrightness;

    void Start()
    {
        LightComponent = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LightComponent.intensity += (targetBrightness - LightComponent.intensity) * slerpSpeed;
    }
}
