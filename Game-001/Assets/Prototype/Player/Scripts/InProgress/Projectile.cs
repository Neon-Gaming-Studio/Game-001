using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    
    public float speed = 0.001f;
    public Transform player;

    private void Start()
    {
        
    }

    void Update ()
    {
        transform.Translate(player.position * speed * Time.deltaTime);
    }
}
