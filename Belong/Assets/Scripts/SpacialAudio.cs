using UnityEngine;

public class SpacialAudio : MonoBehaviour
{
    public AudioSource sound;
    public Transform Listener;
    public float playOffset;
    public float soundFalloff;
    float maxVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        maxVolume = sound.volume;
        sound.PlayDelayed(playOffset);
    }

    // Update is called once per frame
    void Update()
    {
        float dx = (transform.position.x - Listener.position.x) * (transform.position.x - Listener.position.x);
        float dy = (transform.position.y - Listener.position.y) * (transform.position.y - Listener.position.y);
        float dz = (transform.position.z - Listener.position.z) * (transform.position.z - Listener.position.z);
        float hypot = Mathf.Sqrt(dx + dy + dz);
        sound.volume = maxVolume - hypot * soundFalloff;
        sound.panStereo = (transform.position.x - Listener.position.x) / hypot;
    }
}
