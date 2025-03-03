using UnityEngine;

public class CreditScroller : MonoBehaviour
{
    public int sceneToLoad;
    public SceneTransition SceneTransitioner;
    public float scrollSpeed;
    public GameObject skipIcon;
    public SpriteRenderer skipColor;
    public float scrollEnd;

    public float slurp;
    public Vector3 option1;
    public Vector3 option2;
    private Vector3 currDest;
    private float curSlurp;
    private bool eDown;

    float skip;
    bool transitionStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skip = 0;
        currDest = option2;
        transitionStarted = false;
        eDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + scrollSpeed * Time.deltaTime, 1);
        eDown = Input.GetKeyDown("e") || Input.GetKey("e");
        if(eDown && skip < 2) {
            skip += Time.deltaTime;
            skip = Mathf.Min(skip, 2);
            currDest = option1;
        } else if (skip > 0 && skip < 2) {
            skip -= Time.deltaTime;
            skip = Mathf.Max(skip, 0);
            currDest = option2;
        }
        if(transform.position.y > scrollEnd) currDest = option1;
        if(skipIcon.transform.localPosition == currDest) {
            curSlurp = 0;
        } else {
            curSlurp = curSlurp + (slurp - curSlurp) * slurp;
            skipIcon.transform.position = Vector3.Lerp(skipIcon.transform.localPosition, currDest, curSlurp);
        }

        if((skip == 2 || transform.position.y > scrollEnd && eDown) && !transitionStarted) {
            SceneTransitioner.TransitionTo(sceneToLoad);
            transitionStarted = true;
        }
        skipColor.color = new Color32(255, (byte)((2 - skip) / 2 * 255), (byte)((2 - skip) / 2 * 255), 255);
    }
}
