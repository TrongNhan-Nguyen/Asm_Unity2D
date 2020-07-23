using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject player;
    private float currenLocation;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currenLocation = player.transform.localScale.x;
        if (currenLocation > 0)
        {
            rb.velocity = transform.right * speed;
        }
        else if (currenLocation < 0)
        {
            rb.velocity = -(transform.right * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {
            Destroy(gameObject);
            collision.GetComponent<BossHealth>().TakeDamage(1);

        }
    }
}
