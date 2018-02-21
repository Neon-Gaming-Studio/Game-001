using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject projectile;
    public Transform muzzle;

    public void Fire()
    {
        Instantiate(projectile, muzzle.position, Quaternion.identity);
        Debug.Log("Shooting");
    }


}
