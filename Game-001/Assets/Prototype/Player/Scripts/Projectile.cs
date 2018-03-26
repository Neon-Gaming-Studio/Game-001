using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region VARIABLES

    public float speed;
    GameObject player;
    bool facingDir;

    //public GameObject hitFX;

    float damage = 10f;


    #endregion

    #region START METHOD

    void Start()
    {

        //Gets a reference to the players facing direction
        player = GameObject.Find("Player");
        facingDir = player.GetComponent<Player>().FacingDir();
    }

    #endregion

    #region UPDATE METHOD

    void Update()
    {
        //Moves the projectile in the direction the player is facing
        //Reverses the direction of travel if the player is facing backwards.
        if (facingDir)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * -speed * Time.deltaTime);

        }
    }

    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {


        Debug.Log("Hit!");

        //if (collision)
        //{
        //    if (collision.GetComponent<Enemy>())
        //    {

        //        collision.GetComponent<Enemy>().DealDamage(damage);
        //    }
        //}

        //Instantiate(hitFX, transform.position, Quaternion.identity);

        //Destroy(gameObject);

    }
}

