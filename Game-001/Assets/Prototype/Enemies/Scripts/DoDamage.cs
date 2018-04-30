//Does Damage to the Player on Contact
using UnityEngine;

public class DoDamage: MonoBehaviour {

    const float attackDamage = 10f;

    void OnTriggerEnter2D(Collider2D playerContact){

        if(playerContact.gameObject.tag == "Player"){
            Debug.Log("Player Contact!");
            Debug.Log(attackDamage);
            var health = playerContact.gameObject.GetComponent<Health>();
            health.TakeDamage(attackDamage);
        }    

    }   
}