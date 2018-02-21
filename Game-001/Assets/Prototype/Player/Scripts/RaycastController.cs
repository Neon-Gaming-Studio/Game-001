using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*****ATTACHED TO PLAYER*****// 
//This Script Calculates the locations for the Raycasts used for calculating the Colliions in the Controller2D script
    
[RequireComponent(typeof(BoxCollider2D))]

public class RaycastController : MonoBehaviour
{

    #region VARIABLES

    public LayerMask collisionMask;

    [HideInInspector] public BoxCollider2D collider;
    public RaycastOrigins raycastOrigins;


    //Ray Inset from the edge of the Collider to create a skin
    public const float skinWidth = 0.015f;
        
    //Number of Rays
    [HideInInspector] public int horizontalRayCount;
    [HideInInspector] public int verticalRayCount;


    //Distance between the rays (Horizontal and Vertical)
    const float DistBetweenRays = 0.25f;
    [HideInInspector] public float horizontalRaySpacing;
    [HideInInspector] public float verticalRaySpacing;

    #endregion

    #region START METHODS

    public virtual void Awake()
    {
        collider = GetComponent<BoxCollider2D>(); //Get a reference to the Collider attached to the Player
    }

    public virtual void Start ()
    {
        CalculateRaySpacing(); //Calls the Utility Script initially
    }

    #endregion

    #region MAIN METHODS


    //This is called from the Controller2D script every time the Player moves as the values will change as the Character moves
    public void UpdateRaycastOrigins()
    {
        
        //Creates a bounds variable based off the box collider of the Player to store the vector at each of the corners of the collider
        //Reduces the size of the Bounds box by the skin width to create a the origin of the Raycasts from within the collider
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2); 


        //Assigns the values of the corners of the bounds box to the Raycast Origins Struct
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    #endregion

    #region UTILITY METHODS

    //This method is used when this script is inialised to calculate the Rayspacing 
    public void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / DistBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / DistBetweenRays);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    #endregion

    #region STRUCT

    //Creates a struct to store the Vectors of each of the corners of the bounds box
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    #endregion

}
