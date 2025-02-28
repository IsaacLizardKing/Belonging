using System.Collections;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static GameManager Instance { get; private set; }
    void Awake() {
        if(Instance != null)
            Destroy(Instance);
        else
            Instance = this;
        DontDestroyOnLoad(this);
    }

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;

    // delay typing text until after the panel has moved into place
    [SerializeField] float DialoguePanelDelay; 

    // target Lerp speed for the dialogue panel
    [SerializeField] float slurpSpeed; 

    // Desired position for the dialogue panel while on
    [SerializeField] Vector3 DialogueOn; 
    
    // Desired position for the dialogue panel while off
    [SerializeField] Vector3 DialogueOff; 

    

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    public Vector2 playerFacing;
    float delay;
    float curSlurp;


    public void StartDialogue(string[] dialogue, int startPosition, string name)
    {
        nameText.text = name + "...";
        delay = DialoguePanelDelay;
        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(DeployDialogue());
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }

    IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for(int i = startPosition; i < dialogue.Length; i++)
        {
            //dialogueText.text = dialogue[i];
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        EndDialogue();
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
    }

    float charactersPerSecond = 90;

    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator DeployDialogue() {
        while (dialoguePanel.transform.localPosition != DialogueOn) {
            curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
            dialoguePanel.transform.localPosition = Vector3.Lerp(dialoguePanel.transform.localPosition, DialogueOn, curSlurp);
            yield return null;
        }
        curSlurp = 0;
    }

    IEnumerator UndeployDialogue() {
        while (dialoguePanel.transform.localPosition != DialogueOff) {
            curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
            dialoguePanel.transform.localPosition = Vector3.Lerp(dialoguePanel.transform.localPosition, DialogueOff, curSlurp);
            yield return null;
        }
        curSlurp = 0;
    }

    
    void Start() { }

    // Update is called once per frame
    void Update() { }
}