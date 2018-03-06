using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Brackeys Shooting - Amended by MH :)
public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 10;
    public LayerMask notToHit;
          
    float timeToFire = 0;
    Transform firePoint;
    Transform player;

	// Use this for initialization
	void Awake ()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("Did not find FirePoint on Gun");
        }

        player = transform.root.Find("Player");
        if (player == null)
        {
            Debug.Log("Unable to Find Player");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("YButton"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("YButton") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }

        }

        
	}

    void Shoot()
    {
        Debug.Log("Shooting");
    }
}
