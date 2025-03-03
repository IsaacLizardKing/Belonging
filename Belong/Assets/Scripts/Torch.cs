using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightUp : MonoBehaviour
{
    public Light2D light2D;
    public float onIntensity;
    public float offIntensity;
    public float slurpSpeed;
    public float flickerNoiseIntensity;
    public float flickerPeriod;
    public float flickerAmplitude;
    float flickerVariationPhase;
    
    float curSlurp;
    float target;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light2D = GetComponent<Light2D>();
        curSlurp = 0;
        flickerVariationPhase = Random.Range(0, 1/flickerPeriod);
        flickerPeriod += Random.Range(-flickerPeriod / 2, flickerPeriod / 2);
        flickerAmplitude += Random.Range(-flickerAmplitude / 2, flickerAmplitude / 2);

    }
    // Update is called once per frame
    void Update()
    {
        float flickerBacktrack = Random.Range(0, flickerNoiseIntensity) + Mathf.Sin(Time.fixedTime / flickerPeriod) * flickerAmplitude;
        curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
        light2D.intensity += (target - light2D.intensity) * curSlurp - flickerBacktrack * curSlurp;
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        target = 5.0f;
        curSlurp = 0;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        target = 0.5f;
        curSlurp = 0;
    }
}