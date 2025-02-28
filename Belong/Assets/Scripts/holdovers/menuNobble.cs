using UnityEngine;
using UnityEngine.SceneManagement;

public class menuNobble : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float slurp;
    public Vector3 option1;
    public Vector3 option2;
    public int sceneToLoad;
    
    private float curSlurp;
    private Vector3 currDest;

    void Start() {
        curSlurp = 0;
        currDest = option1;
    }

    // Update is called once per frame
    void Update() {
        var vertical = Input.GetAxisRaw("Vertical");
        if(vertical > 0.3f || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            currDest = option1;
        }
        if(vertical < -0.3f || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            currDest = option2;
        }
        if(this.transform.localPosition != currDest) {
            curSlurp = curSlurp + (slurp - curSlurp) * slurp;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, currDest, curSlurp);
        }
        if(this.transform.localPosition == currDest) {
            curSlurp = 0;
        }
        if(Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)) {
            if (currDest == option1) { SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single); }
            else { Debug.Log("Bye Bye"); Application.Quit(); }
        }
    }
}
