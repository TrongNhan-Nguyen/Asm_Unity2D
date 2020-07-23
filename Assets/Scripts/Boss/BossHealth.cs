using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 10;
	public HealthBarBoss healthBar;
	Animator anim;
	
	private void Start()
	{
		healthBar.SetMaxHealth(maxHealth);
		anim = GetComponent<Animator>();
	}
	public void TakeDamage(int damage)
	{
	
		maxHealth -= damage;
		anim.SetTrigger("BossHurt");
		healthBar.SetHealth(maxHealth);
		Debug.Log(maxHealth + "");
		if (maxHealth <= 5)
		{
			anim.SetBool("BossEnrage", true);
			Debug.Log("Boss satrt Strike");
		}

		if (maxHealth <= 0)
		{
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<Boss>().enabled = false;
			GetComponent<BossAttack>().enabled = false;
			this.enabled = false;
			anim.SetBool("BossDie", true);
			Debug.Log("Boss Die");
		}
	}
	public void BossDead()
	{
		SceneManager.LoadScene("ScreenWin");
	}
}
