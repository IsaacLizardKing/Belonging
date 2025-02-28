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
    public float acceltime;
    public float deceltime;
    public float airAccel;
    public float jumpForgiveness;
    
    private bool m_Grounded;

    public UnityEvent OnLandEvent;

    // Tweening Variables
    private float oldHorizontal;
    float start;
    float diff;
    float progress;
    float rate;
    float time;
    float wallx;

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
        horizontal = 0;
        oldHorizontal = 0;
        wallx = transform.position.x - 1000;
    }

    // Update is called once per frame
    void Update()
    {   
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0 && m_Grounded) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (horizontal > 0 && m_Grounded) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        animator.SetFloat("RawHorizontal", horizontal);

        if (Input.GetKeyDown("space") && animator.GetBool("grounded"))
        {
            rb2D.AddForce(Vector2.up * 500);
            animator.SetBool("grounded", false);
            m_Grounded = false; 
        }
        if (animator.GetBool("grounded") && Mathf.Abs(rb2D.linearVelocity.y) > jumpForgiveness) {
            animator.SetBool("grounded", false);
            m_Grounded = false;
        }
        
    }

    void FixedUpdate()
    {
        
        if(oldHorizontal != Mathf.Ceil(Mathf.Abs(horizontal)) * Mathf.Sign(horizontal)) {
            start = rb2D.linearVelocity.x;
            diff = maxSpeed * horizontal - start;
            float speedPercent;
            if(horizontal == 0f) speedPercent = Mathf.Abs(rb2D.linearVelocity.x / maxSpeed);
            else speedPercent = Mathf.Abs((maxSpeed * horizontal - rb2D.linearVelocity.x) / maxSpeed * horizontal);
            if(horizontal == 0f) time = speedPercent * deceltime;
            else time = speedPercent * acceltime;
            if(time == 0f) rate = 0;
            else rate = 1 / time;
            progress = 0;
        }
        if(progress < 1f) {
            progress = progress + rate * Time.deltaTime * (m_Grounded ? 1 : airAccel);
            float x;
            if(progress >= 1f) x = 1;
            else if (progress * progress == 0f) x = 0;
            else x = 1 + (Mathf.Sqrt(1 - ((1 - progress) * (1 - progress))) - 1);
            if(float.IsNaN(x)) Debug.Log("");
            
            float newVelocity = start + x * diff;
            float deltaV = (newVelocity - rb2D.linearVelocity.x);
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x + deltaV, rb2D.linearVelocity.y);
        }
        
        /*if(m_Grounded) {
            if(horizontal == 0f){
                if(rb2D.linearVelocity.x != 0) {
                    var vSign = Mathf.Sign(rb2D.linearVelocity.x);
                    var dvx = Mathf.Clamp(maxSpeed * deceleration * Time.deltaTime, Mathf.Min(rb2D.linearVelocity.x * vSign, rb2D.linearVelocity.x), Mathf.Max(rb2D.linearVelocity.x * vSign, rb2D.linearVelocity.x));
                    rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x - (dvx * vSign), rb2D.linearVelocity.y);
                }
            }
        } else {
            if(Mathf.Abs(rb2D.linearVelocity.x) < maxSpeed * 0.6f || (Mathf.Sign(rb2D.linearVelocity.x) != Mathf.Sign(horizontal))) {
                rb2D.AddForce(Vector2.right * horizontal * 5);
            }
        }*/


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


        if(wallx != transform.position.x){
            oldHorizontal = Mathf.Ceil(Mathf.Abs(horizontal)) * Mathf.Sign(horizontal);
            wallx = transform.position.x - 1000;
        } else {
            oldHorizontal = 0;
        }
	}

    public void Landed() {
        animator.SetBool("grounded", true);
    }

    void OnTriggerEnter2D(Collider2D Other) {
        Debug.Log("Yowch!");
        if(rb2D.linearVelocity.x == 0f) {
            oldHorizontal = 0;
            wallx = transform.position.x;
        }
    }
}