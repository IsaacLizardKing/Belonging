using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Light2D LightComponent;
    private UnityEvent OnTransition;
    
    public float slurpSpeed;
    public float targetBrightness;
    public int sceneTo;

    float curSlurp;

    void Start()
    {
        LightComponent = GetComponent<Light2D>();
        curSlurp = 0;
        OnTransition = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
        LightComponent.intensity += (targetBrightness - LightComponent.intensity) * curSlurp;
        if(Mathf.Abs(LightComponent.intensity - targetBrightness) < 0.0001) {
            OnTransition?.Invoke();
        }
    }
    public void TransitionEvent() {
        SceneManager.LoadScene(sceneTo, LoadSceneMode.Single);
    }
    public void TransitionTo(int scene) {
        targetBrightness = 0;
        slurpSpeed *= 2;
        curSlurp = 0;
        sceneTo = scene;
        OnTransition.AddListener(TransitionEvent);
    }
    public void TransitionTo(){
        slurpSpeed *= 2;
        curSlurp = 0;
        targetBrightness = 0;
        OnTransition.AddListener(TransitionEvent);
    }
}
