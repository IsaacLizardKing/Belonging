using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class TextGlobalLighting : MonoBehaviour
{
    public Light2D globe;
    [SerializeField] TextMeshProUGUI text;
    public float maxBrightness;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(text);
    }

    // Update is called once per frame
    void Update()
    {
        text.color = new Color32((byte)(text.color.r * 255), (byte)(text.color.b * 255), (byte)(text.color.g * 255), (byte)(Mathf.Min(globe.intensity / maxBrightness * 255, 255)));
    }
}
