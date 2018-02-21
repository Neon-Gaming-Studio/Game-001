using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int gunId;
    public string gunName;
    public string gunDesc;
    public float gunDamage;
    public Sprite gunImage;
    public GameObject gunProjectile;


    public Gun(int _gunID, string _gunName, string _gunDesc, float _gunDamage, Sprite _gunImage, GameObject _gunProjectile)
    {
        gunId = _gunID;
        gunName = _gunName;
        gunDesc = _gunDesc;
        gunDamage = _gunDamage;
        gunImage = _gunImage;
        gunProjectile = _gunProjectile;

    }


}
