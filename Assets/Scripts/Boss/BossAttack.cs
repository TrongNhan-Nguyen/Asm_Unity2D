using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
   
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Movement>().TakeDamage(1);
            if (colInfo.GetComponent<Movement>().maxHealth <=0) {
                anim.SetBool("BossWin", true);
            }
        }
    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Movement>().TakeDamage(2);

            if (colInfo.GetComponent<Movement>().maxHealth <= 0)
            {
                anim.SetBool("BossWin", true);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
