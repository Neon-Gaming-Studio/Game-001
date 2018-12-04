using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 ACTION MAPPINGS CREATED IN UNITY - Check EDIT > PROJECT SETTINGS > INPUT
 
 CONTROLLER                        CONTROLLER CODES                                     GAME                          UNITY NAMES               KEYBOARD SHORTCUTS
 
                                   WINDOWS                MACOSX
BUTTONS 

 B = A (XBOX)                      Joystick Button 0      Joystick Button 16            Jump                          BButton                   SPACE
 A = B (XBOX)                      Joystick Button 1      Joystick Button 17            Roll                          AButton                   C
 Y = X (XBOX)                      Joystick Button 2      Joystick Button 19            Shoot                         YButton                   L Mouse
 X = Y (XBOX)                      Joystick Button 3      Joystick Button 19            Change Weapon                 XButton                   Q
 
 Left Shoulder                     Joystick Button 4      Joystick Button 13            ****                          LShoulder                 ****
 Right Shoulder                    Joystick Button 5      Joystick Button 14            Grenade                       RShoulder                 R Mouse
  
 Select                            Joystick Button 6      Joystick Button 10            Map                           Select                    TAB            
 Start                             Joystick Button 7      Joystick Button 9             Menu                          Start                     Enter

 Left Stick Click                  Joystick Button 8      Joystick Button 11            ****                          ****                      ****
 Right Stick Click                 Joystick Button 9      Joystick Button 12            ****                          ****                      ****
 
AXISES

 Left Thumbstick X                 X Axis                 X Axis                        Movement                      LThumbX                   A / D
 Left Thumbstick Y                 Y Axis                 Y Axis                        Movement                      LThumbY                   W / S
 Right Thumbstick X                4th Axis               3rd Axis                      Shooting Dir                  RThumbX                   right
 Right Thumbstick Y                5th Axis               4th Axis                      Shooting Dir                  RThumbY                   up
 D-PAD Left / Right                6th Axis               7 / 8                         Shooting Dir                  DPADX                     right
 D-PAD Up / Down                   7th Axis               5 / 6                         Shooting Dir                  DPADY                     up
 Left Trigger                      9th Axis               5th Axis                      Lock Position                 LTrigger                  Z
 Right Trigger                     10th Axis              6th Axis                      Lock Position                 RTrigger                  X
 Left/Right Trigger Combined       3rd Axis               ****                          ****                          ****                      ****

 */



//This Script controls the Player Input from the Action Mapping Above

//Requires a Player Script to be attached to the same Object. Adds a Player Script if there is not one.
//Also ensure that it cannot be removed if there is still a player Script attach to the same object.
[RequireComponent(typeof(Player))]

public class PlayerInput : MonoBehaviour {

    Player player;
    //Animator animator;    - This will be used for animation
    bool isFacingRight = true;

    //Get a reference to the Player Script
    void Start () {
        player = GetComponent<Player>();
        //animator = GetComponent<Animator>();     - This will be used for animation
    }


    void Update () {

        //Picks up the Horizontal and Vertical input from the Left Thumbstick of the XBOX Controller or the Thumstick of the Switch
        //Used for character directional Movement in the Player Controller script
		Vector2 directionalInput =  new Vector2(Input.GetAxisRaw("LThumbX"), Input.GetAxisRaw("LThumbY"));
        player.SetDirectionalInput(directionalInput, isFacingRight);
        
        Vector2 shootingDirection = new Vector2(Input.GetAxisRaw("RThumbX"),Input.GetAxisRaw("RThumbY"));
        player.SetShootingDirectionInput(shootingDirection);

        //TODO: Remove Right Trigger after testing
        //Right Trigger on Xbox controller
        float rtrigger = Input.GetAxis("RTrigger");
        
        //Picks up the A button on the XBOX Controller or B button on the Switch Controller
        //Used for Jumping in the Player Controller Script   
        if (Input.GetButtonDown("BButton"))
        { //On Press Down
            player.OnJumpInputDown();
            //Debug.Log("Xbox A pressed!");
        }

        if (Input.GetButtonUp("BButton"))
        { //On Lift Up
            player.OnJumpInputUp();
            //Debug.Log("Xbox A released!");
        }

        if (Input.GetButtonDown("AButton"))
        {
            //Debug.Log("Xbox B pressed!");
        }

        if (Input.GetButton("YButton") || rtrigger > 0)
        {
            player.Shoot();
            //Debug.Log("Xbox X pressed!");
        }

        if (Input.GetButtonDown("XButton"))
        {
            //Debug.Log("Xbox Y pressed!");
        }


        if (directionalInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (directionalInput.x < 0 && isFacingRight)
        {
            Flip();
        }

    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;

    }
}
