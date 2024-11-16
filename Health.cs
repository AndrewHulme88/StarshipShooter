using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 50;
    [SerializeField] int currentHealth = 50;
    //[SerializeField] int deathScore = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void IncreaseMaxHealth(int increaseAmount)
    {
        maxHealth += increaseAmount;
        currentHealth = maxHealth;
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            if(gameObject.CompareTag("Enemy"))
            {
                //add Score
            }
            else if(gameObject.CompareTag("Player"))
            {
                //game over
            }
        }
    }
}
