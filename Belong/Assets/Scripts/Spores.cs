using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

public class Spores : MonoBehaviour
{
    public GameObject player;
    private ParticleSystem spores;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spores = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        spores.Play();

        
    }
}
