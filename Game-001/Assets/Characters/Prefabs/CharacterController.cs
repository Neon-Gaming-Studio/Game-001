using UnityEngine;

public class CharacterController : MonoBehaviour {

    [Header("Character Movement")]
    [Header("")]

    //Movement
    public float moveSpeed;

    private float moveHoz;

    bool isFacingRight = true;

    [Header("Character Jumping")]
    [Header("")]
    //Jumping
    [Range(0,10)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isInAir = false;

    //Character Component
    Rigidbody2D rb;
    Animator anim;
    
    //On Awake get get Component References
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        Movemment();
        Jump();     

    }

    //General Character Movement
    void Movemment()
    {

        //Horizontal Movement
        moveHoz = (Input.GetAxis("Horizontal") * moveSpeed) * Time.deltaTime;
        transform.Translate(moveHoz, 0, 0);

        //Facing Direction
        if (moveHoz > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveHoz < 0 && isFacingRight)
        {
            Flip();
        }
        

        //Animation: WALKING
        if (moveHoz != 0 && !isInAir)
        {
            Debug.Log("IS WALKING");
            
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
            
        }


        //Animation: IDLE
        if (moveHoz == 0 && !isInAir)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
        
        

    }

    //Jumping
    void Jump()
    {
        //Jumping Movement 
        if (Input.GetButtonDown("Jump") && !isInAir)
        {
            isInAir = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;

        }
        if (rb.velocity.y == 0)
        {
            isInAir = false;
        }

        //Increased Jump Height Control
        if (rb.velocity.y < 0)
        {
            Debug.Log("High Jump");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            Debug.Log("Low Jump");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        
    }

    
    //Flip Facing Direction
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;

    }


}

