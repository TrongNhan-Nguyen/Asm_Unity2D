using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   
    public Transform attackPoint;
    public float attackRange = 0.45f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    int dealDamage;
    float nextAttakTime = 0f;
    Animator anim;
  
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(setDamage());
    }

    void Update()
    {
        if(Time.time >= nextAttakTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attack();
                nextAttakTime = Time.time + 1f / attackRate;
                PlayerSounds.PlaySounds("attack");
            }
        }
       
    }
    IEnumerator setDamage()
    {
        yield return new WaitForSeconds(1.0f);
        dealDamage = UserInfo.dealDamage;
    }
    private void attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(dealDamage);

            }if(enemy.GetComponent<BossHealth>() != null)
            {
                enemy.GetComponent<BossHealth>().TakeDamage(dealDamage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
//
