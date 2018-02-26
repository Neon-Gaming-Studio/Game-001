//HEALTH SCRIPT
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float maxHealth = 100f;
    public float currentHealth;
    public float overShield = 0f;

    
    public void start(){

        currentHealth = maxHealth;
    
    }

    //*** Take Damage Function ***
    public void TakeDamage(float damageAmount){
        //Need a cue for soundFX for taking damage
        
        currentHealth -= damageAmount;
        //Health bar update

        if (currentHealth <= 0f) {
            currentHealth = 0f;    
            //player dies - destory player object
            //cue for SFX for dying and death music
            //send cue for death animation
            Debug.Log("Game Over!");  
            //Here you would trigger menu options for restarting/quitting/loading
        }
    }

    //*** Healing Function ***
    public void HealthPickup(float healAmount){
        //need a sfx for heath pickup
        if (currentHealth == maxHealth){  //if current health is alreay 100 then no more can be added
            healAmount = 0f;
        } else {              
            if((currentHealth += healAmount) >= maxHealth ){
                currentHealth = maxHealth;
                //sound cue for healing
                //health bar replenish by heal amount
            } else {
                currentHealth += healAmount;
            }
        }
    }

    //*** Max Health Increase Function *** 
    public void MaxHealthIncrease (float increaseAmount) {  
        //sound fx for max health increase
        //health bar changes size  
        maxHealth += increaseAmount;
    }

    //*** Function for Overshield ***
    public void OverShield(){

    }

}