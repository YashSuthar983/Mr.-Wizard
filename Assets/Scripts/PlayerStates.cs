using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace YS
{
    public class PlayerStates : CharacterStates
    {
        public Slider healthBarSlider;
        PlayerMovement PlayerMovement;
        private void Start()
        {
            PlayerMovement = GetComponent<PlayerMovement>();
            currentHealth=maxHealth;
            healthBarSlider.maxValue = maxHealth;
            UpdateHealthSlider(currentHealth);
        }

        public void UpdateHealthSlider(float currentHealth)
        {
            healthBarSlider.value = currentHealth;
        }
        public void TakeDamage(float damage)
        {
            if(currentHealth-damage<=0)
            {
                currentHealth = 0;
                isDead = true;
                PlayerMovement.anim.Play("Dead");
            }
            else
            {
                currentHealth=currentHealth-damage;
            }
            UpdateHealthSlider(currentHealth);
        }
    }
}