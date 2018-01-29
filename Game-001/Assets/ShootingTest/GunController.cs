using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    Vector2 input;
    
    public float horizAxis;
    public float vertAxis;

    KeyButton key;

    float rot = 45f;

    private void Update()
    {
        vertAxis = Input.GetAxisRaw("Vertical");
        horizAxis = Input.GetAxisRaw("Horizontal");

        input = new Vector2(horizAxis, vertAxis);

        if (horizAxis > 0 && vertAxis == 0)  { GunRight();       }
        if (horizAxis > 0 && vertAxis > 0)   { GunTopRight();    }
        if (horizAxis > 0 && vertAxis < 0)   { GunBottomRight(); }

        if (horizAxis < 0 && vertAxis == 0)  { GunLeft();        }
        if (horizAxis < 0 && vertAxis > 0)   { GunTopLeft();     }
        if (horizAxis < 0 && vertAxis < 0)   { GunBottomLeft();  }

        if (horizAxis == 0 && vertAxis > 0)  { GunTop();         }
        if (horizAxis == 0 && vertAxis < 0)  { GunBottom();      }

        



    }

    void GunRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        key.ResetKeys();
        key.right = true;
    }

    void GunTopRight()
    {
        transform.eulerAngles = new Vector3(0,0,45);
        key.ResetKeys();
        key.up = true;
        key.right = true;
    }

    void GunBottomRight()
    {
        transform.eulerAngles = new Vector3(0, 0, -45);
        key.ResetKeys();
        key.down = true;
        key.right = true;
    }

    void GunLeft()
    {
        transform.eulerAngles = new Vector3(0, 0, 180);
        key.ResetKeys();
        key.left = true;
    }

    void GunTopLeft()
    {
        transform.eulerAngles = new Vector3(0, 0, 135);
        key.ResetKeys();
        key.left = true;
        key.up = true;
    }

    void GunBottomLeft()
    {
        transform.eulerAngles = new Vector3(0, 0, -135);
        key.ResetKeys();
        key.left = true;
        key.down = true;
    }

    void GunBottom()
    {
        transform.eulerAngles = new Vector3(0, 0, -90);
        key.ResetKeys();
        key.down = true;
    }

    void GunTop()
    {
        transform.eulerAngles = new Vector3(0, 0, 90);
        key.ResetKeys();
        key.up = true;
    }







    public struct KeyButton
    {
        public bool left, right;
        public bool up, down;
        public bool angleUpRight, angleBottomRight;
        public bool angleUpLeft, angleBottomLeft;


        public void ResetKeys()
        {

            left = right = up = down = angleUpRight = angleBottomRight = angleUpLeft = angleBottomLeft = false;      
            
        }

    }

}
