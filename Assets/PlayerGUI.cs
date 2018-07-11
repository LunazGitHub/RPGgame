using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{

    public int MaxHealth = 100;
    public int CurrentHealth = 100;

    public float CurrentMana = 100.0f;
    public int MaxMana = 100;

    public float CurrentStamina = 100.0f;
    public int MaxStamina = 100;

    public float CurrentExperience = 0.0f;
    public int MaxExperience = 100;

    PlayerMovement playermovement;
    
    private float BarLength = 0.0f;


    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);


    bool isDead;
    bool damaged;


	// Use this for initialization
	void Start ()
    {
        playermovement = GetComponent<PlayerMovement>();
        
        BarLength = Screen.width / 8;
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        AdjustCurrentHealth(0);
        AdjustCurrentMana(0);

        if(damaged)
        {
            damageImage.color = flashColor;
        }

        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
		

        if(CurrentMana >= 0)
        {
            CurrentMana += Time.deltaTime;
        }

        if(CurrentMana >= MaxMana)
        {
            CurrentMana = MaxMana;
        }

        if(CurrentMana <= 0)
        {
            CurrentMana = 0;
        }

        damaged = false;

	}

    public void TakeDamage(int amount)
    {

        damaged = true;

        CurrentHealth -= amount;

        

        if(CurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        
        playermovement.enabled = false;
    }



    private void OnGUI()
    {
        GUI.Box(new Rect(5, 30, 80, 20), "HP");
        GUI.Box(new Rect(5, 50, 80, 20), "Mana");
       
        GUI.Box(new Rect(85, 30, BarLength, 20), CurrentHealth.ToString("0") + "/" + MaxHealth);
        GUI.Box(new Rect(85, 50, BarLength, 20), CurrentMana.ToString("0") + "/" + MaxMana);
       
    }


    public void AdjustCurrentHealth(int adj)
    {
        CurrentHealth += adj;

        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }

    }

    public void AdjustCurrentMana(int adj)
    {

        CurrentMana += adj;

    }

}
