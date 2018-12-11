using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int Health = 5;

    public void DoDamage()
    {

        Health -= 1; 

    }

    public void Update()
    {
        if (Health <= 0)
        {
            Debug.Log("Enemy Died");
            Destroy(gameObject);
        }
    }
}
