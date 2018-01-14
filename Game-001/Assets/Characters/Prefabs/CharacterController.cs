using UnityEngine;

public class CharacterController : MonoBehaviour {

    [Header("Character Movement")]
    [Header("")]

    //Movement
    public float moveSpeed;


    //Jumping
    [Range(0,10)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool isInAir = false;

    //Character Component
    Rigidbody2D rb;
    
    //On Awake get get the Characters Rigidbody Component
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update ()
    {

        Movemment();
        Jump();     

    }


    void Movemment()
    {

        //Horizontal Movement
        float moveHoz = (Input.GetAxis("Horizontal") * moveSpeed) * Time.deltaTime;
        transform.Translate(moveHoz, 0, 0);


    }

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



}

