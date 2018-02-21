using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {
/*
    Player player;
    Controller2D controller;

    public float wallSlideSpeedMax = 3;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeapOff;

    public float wallStickTime;
    float timeToWallUnStick;
    bool wallSliding;
    int wallDirX;

    Vector3 velocity;
    Vector2 directionalInput;

    public bool enableWallClimb;

    void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<Controller2D>();
    }  


    void Update()
    {
        
        if (enableWallClimb)
        {
            HandleWallSliding();
        }

        //velocity = player.velocity;
        //directionalInput = player.directionalInput;


        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeapOff.y;
                velocity.y = wallLeapOff.y;
            }
        }
    }
     
    public void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;

        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;
            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }
            if (timeToWallUnStick > 0)
            {
                //player.velocityXSmoothing = 0;
                velocity.x = 0;
                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnStick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnStick = wallStickTime;
                }

            }
            else
            {
                timeToWallUnStick = wallStickTime;
            }
        }

    }*/
}
