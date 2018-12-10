﻿using UnityEngine;

//The Controller Script derives from the RaycastController 
//It used used for all the PLAYER and calculate the COLLISIONS

public class Controller : RaycastController
{
    #region VARIABLES
    
    //Maximun Angle the Player can walk up
    float maxSlopeAngle = 80f;

    Vector2 moveAmount;

    float gravity;
    Vector3 velocity;
    float velocityXSmoothing;
    float maxJumpVelocity;
    float minJumpVelocity;

    //Component References
    public CollisionInfo collisions;
    private Player player;

    //Input value
    [HideInInspector]
    public Vector2 playerInput;

    private Animator animator;

    #endregion

    #region START

    public override void Start() {
        base.Start();
        collisions.faceDir = 1;

        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();

        //Used for Jumping
        CalculateJumping();

    }

    #endregion

    #region UPDATE

    public void Update()
    {
        Move(playerInput);
        SlopeSlideCheck();
    }

    #endregion

    #region CHARACTER MOVEMENT

    //Moves the player with the plateform 
    public void Move(bool standingOnPlatform) {
        Move(Vector2.zero, standingOnPlatform);
    }
            
    //Main Player MOVE Method
    public void Move(Vector2 input, bool standingOnPlatform = false) {
        //Calculates the velocity of the player
        moveAmount = CalculateVelocity(input) * Time.deltaTime;      

        UpdateRaycastOrigins(); //Updates the Raycast Origins of the player box collider bounds in the RaycastController Script 
        collisions.Reset();
        collisions.moveAmountOld = moveAmount;
        playerInput = input;
        

        if (moveAmount.y < 0) {
            DescendSlope(ref moveAmount);
        }

        if (moveAmount.x != 0) {
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
        }

        //Animator Controller
        if (moveAmount.x > 0.1f || moveAmount.x < -0.1f) {
            animator.SetInteger("State", 1);
        }
        else if (moveAmount.x <= 0.1f && moveAmount.x >= -0.1f) {
            animator.SetInteger("State", 0);
        }

        HorizontalCollisions(ref moveAmount);
    
        if (moveAmount.y != 0) {
            VerticalCollisions(ref moveAmount);
        }

        if (standingOnPlatform) {
            collisions.below = true;
        }

        //============MOVES THE PLAYER==============//
        transform.Translate(moveAmount);
        //============MOVES THE PLAYER==============//
    }

    #endregion

    #region JUMP METHODS

    private void CalculateJumping() {
        gravity = -(2 * player.maxJumpHeight) / Mathf.Pow(player.timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * player.timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * player.minJumpHeight);
    }

    public void OnJumpInputDown() {


        if (collisions.below) {
            if (collisions.slidingDownMaxSlope) {
                if (playerInput.x != -Mathf.Sign(collisions.slopeNormal.x)) { 
                    // not jumping against max slope
                    velocity.y = maxJumpVelocity * collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * collisions.slopeNormal.x;
                }
            }
            else {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    public void OnJumpInputUp() {
        if (velocity.y > minJumpVelocity) {
            velocity.y = minJumpVelocity;
        }
    }

    #endregion

    #region COLLISION DETECTION

    //Horizontal
    void HorizontalCollisions(ref Vector2 moveAmount) {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        //Added for WallJump
        if (Mathf.Abs(moveAmount.x) < skinWidth) {
            rayLength = 2 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit) {
                if (hit.distance == 0) { continue; }

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngle <= maxSlopeAngle) {
                    if (collisions.descendingSlope) {
                        collisions.descendingSlope = false;
                        moveAmount = collisions.moveAmountOld;
                    }

                    float distanceToSlopeStart = 0;

                    if (slopeAngle != collisions.slopeAngleOld) {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        moveAmount.x -= distanceToSlopeStart * directionX;
                    }

                    ClimbSlope(ref moveAmount, slopeAngle, hit.normal);
                    moveAmount.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxSlopeAngle) {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope) {
                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    //VERTICAL COLLISIONS - Both above and below the player 
    //Called from the MOVE method
    void VerticalCollisions(ref Vector2 moveAmount) {

        float directionY = Mathf.Sign(moveAmount.y); //Direction of the Y movement (DOWN = -1 / UP = 1)
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth; //Length of the Ray
        
        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft; //Changes the Rays so if the player is moving up they are at the top and vice verse
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x); //Updates the Ray Origin for each ray

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask); //Dreates a RayCast from the Ray Origin and returns if it hits anything

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red); //Draws the Debug line so that it can be seen in the Scene

            //This IF statement determines what to do if the Raycast hits anything
            if (hit) {
                //This IF statement checks the Object the raycast hit for the Through TAG
                //Used for falling through platforms
                if (hit.collider.tag == "Through") {
                    if (directionY == 1 ||  hit.distance == 0) { continue; }
                    if (collisions.fallingThroughPlatform) { continue; }
                    if (playerInput.y == -1) {
                        collisions.fallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform",0.5f);
                        continue;
                    }
                }

                //Stops the movement
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance; //Changes the Ray Length to the distance hit so that it does not try to move the object to a collision further away

                if (collisions.climbingSlope) {
                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
                }

                collisions.above = directionY == 1;
                collisions.below = directionY == -1; 
            }
        }

        if (collisions.climbingSlope) {
            float directionX = Mathf.Sign(moveAmount.x);
            rayLength = Mathf.Abs(moveAmount.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, collisionMask);

            if (hit) {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle) {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                    collisions.slopeNormal = hit.normal;
                }
            }
        }
    }

    #endregion

    #region SLOPE CALCULATIONS

    //Climbing Slopes
    void ClimbSlope(ref Vector2 moveAmount, float slopeAngle, Vector2 slopeNormal) {
        float moveDistance = Mathf.Abs(moveAmount.x);
        float climboveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (moveAmount.y <= climboveAmountY) {
            moveAmount.y = climboveAmountY;
            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
            collisions.slopeNormal = slopeNormal;
        }
    }

    //Descending Slopes
    void DescendSlope(ref Vector2 moveAmount) {

        RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);
        RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);
        if (maxSlopeHitLeft ^ maxSlopeHitRight) {
            SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount);
            SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount);
        }

        if (!collisions.slidingDownMaxSlope) {
            float directionX = Mathf.Sign(moveAmount.x);
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

            if (hit) {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != 0 && slopeAngle <= maxSlopeAngle) {
                    if (Mathf.Sign(hit.normal.x) == directionX) {
                        if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x)) {
                            float moveDistance = Mathf.Abs(moveAmount.x);
                            float descendmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                            moveAmount.y -= descendmoveAmountY;

                            collisions.slopeAngle = slopeAngle;
                            collisions.descendingSlope = true;
                            collisions.below = true;
                            collisions.slopeNormal = hit.normal;
                        }
                    }
                }
            }
        }
    }

    void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount) {

        if (hit) {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle > maxSlopeAngle) {
                moveAmount.x = Mathf.Sign(hit.normal.x) * (Mathf.Abs(moveAmount.y) - hit.distance) / Mathf.Tan(slopeAngle * Mathf.Deg2Rad);

                collisions.slopeAngle = slopeAngle;
                collisions.slidingDownMaxSlope = true;
                collisions.slopeNormal = hit.normal;
            }
        }
    }

    #endregion

    #region UTILITY METHODS

    private void ResetFallingThroughPlatform() {
        collisions.fallingThroughPlatform = false;
    }

    //Called by the Update Method to adjust the velocity of the Player movement
    Vector2 CalculateVelocity(Vector2 _directionalInput) {

        float targetVelocityX = _directionalInput.x * player.moveSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (collisions.below) ? player.accelerationTimeGrounded : player.accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;

        return velocity;
    }

    //This takes the Movement input from the PlayerInput script which is then manipulated by the CalculateVelocity method and passed into the Move method
    //on the Controlled2D script.
    public void SetDirectionalInput(Vector2 _input, bool facingRight) {
        playerInput = _input;
        player.isFacingRight = facingRight;
    }

    //Checks whether the player is on a slope
    private void SlopeSlideCheck() {
        if (collisions.above || collisions.below) {
            if (collisions.slidingDownMaxSlope) {
                velocity.y += collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else {
                velocity.y = 0;
            }
        }
    }

    #endregion

    #region COLLISION STRUCT

    public struct CollisionInfo {

        public bool above, below;
        public bool left, right;

        public bool climbingSlope, descendingSlope;
        public float slopeAngle, slopeAngleOld;

        public Vector2 moveAmountOld;

        public int faceDir; 

        public bool fallingThroughPlatform;
        public bool slidingDownMaxSlope;

        public Vector2 slopeNormal;


        public void Reset() {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;
            slopeNormal = Vector2.zero;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
    #endregion
}
