using UnityEngine;

public class Sliide : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float curSlurp;

    public float slurp;
    public GameObject mover;
    public Vector3 targetPos;
    
    void Start()
    {
        curSlurp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(mover != null){
            if(this.transform.position.y < 0) {
                curSlurp = curSlurp + (slurp - curSlurp) * slurp;
                mover.transform.position = Vector3.Lerp(mover.transform.position, targetPos, curSlurp);
            }
        }
    }
}
