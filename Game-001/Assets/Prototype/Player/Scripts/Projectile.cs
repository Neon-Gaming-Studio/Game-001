using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region VARIABLES

    public float speed;
    GameObject player;
    bool facingDir;

    //TODO Add damage to Weapons
    float damage = 10f;

    #endregion

    #region START METHOD

    void Start() {
        //Gets a reference to the players facing direction
        player = GameObject.Find("Player");
        facingDir = player.GetComponent<Player>().FacingDir();
    }

    #endregion

    #region UPDATE METHOD

    void Update() {
        //Moves the projectile in the direction the player is facing
        //Reverses the direction of travel if the player is facing backwards.
        if (facingDir) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        } else {
            transform.Translate(Vector2.right * -speed * Time.deltaTime);
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision) { 
        //TODO Add functionality for collisions
        Destroy(gameObject);
    }
}

