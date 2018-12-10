using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]

//This is script contains all the Player Character related variables 

public class Player : MonoBehaviour {

    #region VARIABLES

    [Header("Movement Variables")]
    public float moveSpeed;
    Vector2 directionalInput;
    [HideInInspector]
    public bool isFacingRight;

    [Header("Jumping Variables")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = 0.4f;
    [HideInInspector]
    public float accelerationTimeAirborne = 0.2f;
    [HideInInspector]
    public float accelerationTimeGrounded = 0.1f;

    [Header("Shooting Variables")]
    public Vector2 shootingDirection;
    public float fireRate;
    private float fireDelay;
    public GameObject firePoint;
    public GameObject rotator;
    public GameObject projectilePrefab;

    //public bool enableWallClimb = false;

    #endregion

    #region START

    void Start()
    {
        firePoint = GameObject.Find("FirePoint");
        rotator = GameObject.Find("Rotator");
    }

    #endregion

    #region UPDATE

    void Update()
    {
        CalculateShootingDirection();
    }

    #endregion

    #region SHOOT METHODS

    public void Shoot()
    {
        Debug.Log("FIRE!");
        if (Time.time > fireDelay)
        {
            fireDelay = Time.time + fireRate;
            GameObject bullet = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);

        }
    }


    void CalculateShootingDirection()
    {
        if (!isFacingRight)
        {
            shootingDirection = -shootingDirection;
        }

        if (shootingDirection == Vector2.zero)
        {
            shootingDirection = new Vector2(1, 0);
        }

        if (shootingDirection.x == -1)
        {
            shootingDirection.x = 0;
        }

        float shootingAngle = (Mathf.Rad2Deg * (Mathf.Atan2(shootingDirection.y, shootingDirection.x)));
        rotator.transform.rotation = Quaternion.Euler(0, 0, shootingAngle);
    }

    #endregion

    #region UTILITY METHODS

    //This takes the directional Input from the PlayerInput script
    public void SetShootingDirectionInput(Vector2 inputShooting)
    {
        shootingDirection = inputShooting;
    }

    //Allows the projectile to access the facing direction
    public bool FacingDir()
    {
        return isFacingRight;
    }

    

   
    #endregion

}
