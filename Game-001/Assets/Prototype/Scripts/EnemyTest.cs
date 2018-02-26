//ENEMY TEST
using UnityEngine;

public class EnemyTest: MonoBehaviour {

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