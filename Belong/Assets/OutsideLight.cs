using UnityEngine;
using UnityEngine.Rendering.Universal;


public class OutsideLight : MonoBehaviour
{

    private Light2D LightComponent;
    public Light2D GlobalLight;
    public SceneTransition st;
    public float maxBrightness;
    float otherMax;

    void Start()
    {
        LightComponent = GetComponent<Light2D>();
        otherMax = st.targetBrightness;
    }

    // Update is called once per frame
    void Update()
    {
        LightComponent.intensity = (GlobalLight.intensity / otherMax) * maxBrightness;
    }
}
