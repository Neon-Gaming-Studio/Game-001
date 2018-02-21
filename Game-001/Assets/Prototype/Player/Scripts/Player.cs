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

    void Start() {

        controller = GetComponent<Controller2D>();
        animController = GetComponent<PlayerAnimController>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

    }

    #endregion

    #region UPDATE METHOD

    void Update() {

        CalculateVelocity();

        //animConntroller.RunningAction(Mathf.Abs(velocity.x) / moveSpeed);
        //animConntroller.Jump(velocity.y);  
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

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    void CalculateVelocity() {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }
    #endregion

}
