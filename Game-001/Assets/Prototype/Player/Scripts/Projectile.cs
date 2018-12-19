using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region VARIABLES

    public float speed;
    GameObject player;
    bool facingDir;

    //TODO : Variables - Add damage to Weapons
    //float damage = 10f;

    #endregion

    #region START METHOD

    void Start() {
        //Gets a reference to the players facing direction
        facingDir = GameObject.Find("Player").GetComponent<Controller>().FacingDir();
        if (!facingDir)
        {
            speed = -speed;
        }

        Invoke("DestroyProjectile",2f);
    }

    #endregion

    #region UPDATE METHOD

    void Update() {          
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision) {
        //TODO : OnTriggerEnter2D - Add functionality for collisions
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().DoDamage();
            Debug.Log("Hit Enemy!");
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

