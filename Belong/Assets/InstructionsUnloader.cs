using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsUnloader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D Other) {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
