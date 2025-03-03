using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsUnloader : MonoBehaviour
{
    public int sceneToLoad;
    public SceneTransition SceneTransitioner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        SceneTransitioner.TransitionTo(sceneToLoad);
    }
}
