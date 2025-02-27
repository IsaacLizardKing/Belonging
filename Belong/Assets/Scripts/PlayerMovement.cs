using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;

    public float runSpeed = 5f;
    private bool m_Grounded;

    public UnityEvent OnLandEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnLandEvent == null) {
		    OnLandEvent = new UnityEvent();
        }
        OnLandEvent.AddListener(Landed);
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0) {
            spriteRenderer.flipX = true;
        } else if (horizontal > 0) {
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown("space") && animator.GetBool("grounded"))
        {
            rb2D.AddForce(Vector2.up * 500);
            animator.SetBool("grounded",false);
            Debug.Log("space key was pressed");
        }

        
    }

    void FixedUpdate()
    {
        if(m_Grounded) {
            rb2D.linearVelocity = new Vector2(horizontal * runSpeed, rb2D.linearVelocity.y);
        } else {
            if(rb2D.linearVelocity.x < runSpeed) {
                rb2D.AddForce(Vector2.right * horizontal * 10);
            }
        }


		bool wasGrounded = m_Grounded;

        m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
                m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
        animator.SetFloat("Vertical", rb2D.linearVelocity.y);
        animator.SetFloat("Horizontal", rb2D.linearVelocity.x);
	}

    public void Landed() {
        animator.SetBool("grounded", true);
    }

}