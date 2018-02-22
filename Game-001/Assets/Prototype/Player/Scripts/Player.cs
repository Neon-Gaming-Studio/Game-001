using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]

public class Player : MonoBehaviour {

    #region VARIABLES

    [Header("Movement Variables")]
    public float moveSpeed;
    float gravity;
    Vector3 velocity;
    float velocityXSmoothing;
    Vector2 directionalInput;

    [Header("Jumping Variables")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = 0.4f;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float maxJumpVelocity;
    float minJumpVelocity;
    
    //Component References 
    Controller2D controller;
    PlayerAnimController animController;

    //public bool enableWallClimb = false;

    #endregion

    #region START METHODS

    void Start()
    {

        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

    }

    #endregion

    #region UPDATE METHOD

    void Update()
    {
        //Each Update it calculates the velocity of the player
        CalculateVelocity();

        //Calls the Move Method on the Controller2D script to move the Character 
        controller.Move(velocity * Time.deltaTime, directionalInput);


        if (controller.collisions.above || controller.collisions.below)
        {
           
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
      
    }

    #endregion

    #region JUMP METHODS


    public void OnJumpInputDown() {
        
       
        if (controller.collisions.below)
        
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
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

    #region UTILITY METHODS

    //This takes the Movement input from the PlayerInput script which is then manipulated by the CalculateVelocity method and passed into the Move method
    //on the Controlled2D script.
    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }


    //Called by the Update Method to adjust the velocity of the Player movement
    void CalculateVelocity()
    {
        //Adds a smooth calculation to the horizontal movement 
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        //Calculates Gravity against the player
        velocity.y += gravity * Time.deltaTime;
    }
    #endregion

}
