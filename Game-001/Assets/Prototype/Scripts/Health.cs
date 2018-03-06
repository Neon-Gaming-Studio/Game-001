//HEALTH SCRIPT
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float maxHealth = 100f;
    public float currentHealth;
    public float overShield = 0f;
    public Slider healthBar; 
    public float Damage;

    public void Start(){

        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }

    public void Update(){
        
        //healthBar.value = (currentHealth/100);
        // Debug.Log("current health on update: " + currentHealth);    
    }

    //*** Take Damage Function ***
    public void TakeDamage(float damageAmount){
        //Need a cue for soundFX for taking damage
        //Debug.Log(damageAmount);
        currentHealth -= damageAmount;
        
        //Health bar update
        healthBar.value = (currentHealth/100);

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
        } else if((currentHealth + healAmount) >= maxHealth) {   //if below 100 but pickup makes it go above 100, capped at 100         
            currentHealth = maxHealth;
            healthBar.value = (currentHealth/100);
        } else {
            currentHealth += healAmount;
            healthBar.value = (currentHealth/100);
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