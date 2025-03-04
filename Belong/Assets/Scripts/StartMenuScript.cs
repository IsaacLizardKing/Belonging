using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float destination;
    public float slurpSpeed;
    public float maxSpeed;
    
    public GameObject background4;
    public float speed4;
    public GameObject background3;
    public float speed3;
    public GameObject background2;
    public float speed2;
    public GameObject background1;
    public float speed1;
    public GameObject background0;
    public float speed0;

    float curSlurp;


    void Start()
    {
        curSlurp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
        float cameraSpeed = Mathf.Min(maxSpeed, (destination - this.transform.position.x) * curSlurp);
        this.transform.position = new Vector3(this.transform.position.x + cameraSpeed, this.transform.position.y, this.transform.position.z);
        background0.transform.position = new Vector3(background0.transform.position.x + cameraSpeed * speed0, background0.transform.position.y, background0.transform.position.z);
        background1.transform.position = new Vector3(background1.transform.position.x + cameraSpeed * speed1, background1.transform.position.y, background1.transform.position.z);
        background2.transform.position = new Vector3(background2.transform.position.x + cameraSpeed * speed2, background2.transform.position.y, background2.transform.position.z);
        background3.transform.position = new Vector3(background3.transform.position.x + cameraSpeed * speed3, background3.transform.position.y, background3.transform.position.z);
        background4.transform.position = new Vector3(background4.transform.position.x + cameraSpeed * speed4, background4.transform.position.y, background4.transform.position.z);
    }
}
