//Pickup (Health)
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour {

    public float healAmount = 10f;

    void OnTriggerEnter2D(Collider2D playerContact){

        if(playerContact.gameObject.tag == "Player"){
            // Debug.Log("Health Collected!");
            // Debug.Log(healAmount);   
            var health = playerContact.gameObject.GetComponent<Health>();
            health.HealthPickup(healAmount);
            Destroy(gameObject);
        }    

    }   

}