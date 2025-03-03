using UnityEngine;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] float talkDistance = 2;
    bool inConversation;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("Interact");
        if (inConversation)
        {
            Debug.Log("Skipping Line");
            TutorialManager.Instance.SkipLine();
        }
        else
        {
            Debug.Log("Looking for NPC");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, talkDistance, Vector2.up, 0, LayerMask.GetMask("NPC"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                if (hit.collider.gameObject.TryGetComponent(out NPC npc))
                {
                    Debug.Log("Got Component");
                    
                    TutorialManager.Instance.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName);
                }
            }
        }
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        TutorialManager.OnDialogueStarted += JoinConversation;
        TutorialManager.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        TutorialManager.OnDialogueStarted -= JoinConversation;
        TutorialManager.OnDialogueEnded -= LeaveConversation;
    }

}