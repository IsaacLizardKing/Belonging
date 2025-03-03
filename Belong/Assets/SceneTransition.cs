using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SceneTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Light2D LightComponent;
    
    public float slurpSpeed;
    float curSlurp;
    public float targetBrightness;

    void Start()
    {
        LightComponent = GetComponent<Light2D>();
        curSlurp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
        LightComponent.intensity += (targetBrightness - LightComponent.intensity) * curSlurp;
    }
}
