using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {


    public int enemyHealth = 100;
    public int currentHealth;
    public Slider EnemySlider;
    
    
    void Awake ()
    {
        
        currentHealth = enemyHealth;
	}
	
	
	void Update ()
    {

        AddJustCurrentHealth(0);
		
	}

    public void TakeDamage(int amount)
    {

        currentHealth -= amount;
        EnemySlider.value = currentHealth;
        
        
    }

    
   void AddJustCurrentHealth(int adj)
    {
        currentHealth += adj;

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        if(currentHealth > enemyHealth)
        {
            currentHealth = enemyHealth;
        }

        if(enemyHealth < 0)
        {

            enemyHealth = 0;
        }

        //healthBarLength = (Screen.width / 4) * (currentHealth / (float)enemyHealth);
    }
    
}
