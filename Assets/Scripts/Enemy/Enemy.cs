using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    public String typeEnemy;
    public Animator anim;
    public static int enemyRemains;
    public static int coins;
    

    void Start()
    {
        coins = 0;
        currentHealth = maxHealth;
       if(SceneManager.GetActiveScene().name == "Screen1")
        {
            enemyRemains = 6;
        }
        if (SceneManager.GetActiveScene().name == "Screen2")
        {
            enemyRemains = 8;
        }
    }

    public void TakeDamage(int damage)
    {
       
        if (typeEnemy == "Demon")
        {
            DemonSounds.PlaySounds("hurt");
            currentHealth -= damage;
            anim.SetTrigger("Hurt");
            
        }
        if (typeEnemy == "Dragon")
        {
            DragonSounds.PlaySounds("hurt");
            currentHealth -= damage;
            anim.SetTrigger("DragonHurt");
        }

        if (currentHealth <= 0)
        {
            coins += 10;
            enemyRemains -= 1;
            PlayerPrefs.SetInt("Scores", enemyRemains);
            Die();
        }
    }

    private void Die()
    {
        if (typeEnemy == "Demon")
        {
            DemonSounds.PlaySounds("die");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<AIEnemy>().enabled = false;
            anim.SetBool("Dead", true);
            this.enabled = false;
        }
        if (typeEnemy == "Dragon")
        {
            DragonSounds.PlaySounds("die");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Dragon_AI>().enabled = false;
            anim.SetBool("DragonDead", true);
            this.enabled = false;
          
        }

     
    }
    public void AfterDead()
    {
        Destroy(gameObject);

    }
}
