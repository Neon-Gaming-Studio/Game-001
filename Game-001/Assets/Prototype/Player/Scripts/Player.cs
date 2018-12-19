using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]

//This is script contains all the Player Character related variables 

public class Player : MonoBehaviour {

    #region VARIABLES

    [Header("Movement Variables")]
    public float moveSpeed;

    [Header("Jumping Variables")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = 0.4f;
    [HideInInspector]
    public float accelerationTimeAirborne = 0.2f;
    [HideInInspector]
    public float accelerationTimeGrounded = 0.1f;

   
    //public bool enableWallClimb = false;

    #endregion
}
