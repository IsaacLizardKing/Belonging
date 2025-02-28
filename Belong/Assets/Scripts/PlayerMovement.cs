using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;

    public float runSpeed = 5f;
    private bool m_Grounded;

    public UnityEvent OnLandEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        } else {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat("horizontal", horizontal);
        if (Input.GetKeyDown("space") && animator.GetBool("grounded"))
        {
            rigidbody2D.AddForce(Vector2.up * 500);
            animator.SetBool("grounded",false);
            Debug.Log("space key was pressed");
        }

        
    }

    void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(horizontal * runSpeed, rigidbody2D.linearVelocity.y);
    
		bool wasGrounded = m_Grounded;
;
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
        animator.SetFloat("Vertical", rigidbody2D.linearVelocity.y);
        animator.SetFloat("Horizontal", rigidbody2D.linearVelocity.x);
	}

    public void Landed() {
        animator.SetBool("grounded", true);
    }

}

/*

using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;

    public float maxSpeed;
    public float acceleration;
    public float deceleration;
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
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (horizontal > 0) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        animator.SetFloat("RawHorizontal", horizontal);

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
            if(horizontal == 0f){

            } else {
                rb2D.linearVelocity = new Vector2(Mathf.Clamp(rb2D.linearVelocity.x + horizontal * maxSpeed * acceleration * Time.deltaTime, -maxSpeed, maxSpeed), rb2D.linearVelocity.y);
            }
        } else {
            if(Mathf.Abs(rb2D.linearVelocity.x) < maxSpeed * 0.6f || (Mathf.Sign(rb2D.linearVelocity.x) != Mathf.Sign(horizontal))) {
                rb2D.AddForce(Vector2.right * horizontal * 5);
            }
        }


		bool wasGrounded = m_Grounded;

        m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCapsuleAll(transform.position, new Vector2(0.313f, 0.1f), CapsuleDirection2D.Horizontal, 90f);
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
*/