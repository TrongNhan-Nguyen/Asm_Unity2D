﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    //public float speed = 1f;
    //public float distance;
    ////

    //bool movingRight = true;
    //public Transform groudDetection;

    //void Update()
    //{
    //    transform.Translate(Vector2.right * speed * Time.deltaTime);
    //    RaycastHit2D groundInfo = Physics2D.Raycast(groudDetection.position, Vector2.down, distance);
    //    if(groundInfo.collider == false)
    //    {
    //        if(movingRight == true)
    //        {
    //            transform.eulerAngles = new Vector3(0, -180, 0);
    //            movingRight = false;
    //        }
    //        else
    //        {
    //            transform.eulerAngles = new Vector3(0, 0, 0);
    //            movingRight = true;

    //        }
    //    }

    //}


    #region Public Variabales
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public int maxHealth;
    public float attackDistance; // Minium distance for attack
    public float moveSpeed;
    public float timer; // Timer for coolldown between attack
    #endregion

    #region Private Vatiables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; // Store the distance between enemy and player
    private float intTimer;
    private bool attackMode;
    private bool inRange; // Check if Player in range;
    private bool cooling; // Check if Enemy is cooling after attack;
    public Transform LeftLimit;
    public Transform RightLimit;
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!attackMode)
        {
            Move();
           
        }

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RayCastDebugger();
        }
        // When Player is detected
        if (hit.collider != null)
        {
            EnemyLogic();
            moveSpeed = 3f;
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }
        if (inRange == false)
        {
          
            StopAttack();
            moveSpeed = 2f;
        }
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, LeftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, RightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = LeftLimit;
        }
        else
        {
            target = RightLimit;
        }
        Flip();
    }

    private void Flip()
    {
        Vector3 rotaion = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotaion.y = 180f;
        }
        else
        {
            rotaion.y = 0;
        }
        transform.eulerAngles = rotaion;

    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
        {
          
            StopAttack();
        }
        else if (distance <= attackDistance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("EnemyAttack", false);

        }
    }

    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("EnemyAttack", false);
    }

    private void Attack()
    {
       
        timer = intTimer; //Reset timer when player eneter Attack range
        attackMode = true; //To check if enemy still attack or not
        anim.SetBool("EnemyWalk", false);
        anim.SetBool("EnemyAttack", true);
       
    }

    private void Move()
    {
        anim.SetBool("EnemyWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack"))
        {
            Vector2 tagetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, tagetPosition, moveSpeed * Time.deltaTime);
        }
    }
    private void Cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && attackMode)
        {
            DemonSounds.PlaySounds("attack");
            cooling = false;
            timer = intTimer;
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
    private void RayCastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);

        }
        else if (distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.transform;
            inRange = true;
            Flip();
            
        }
    }
    private bool InsideofLimits()
    {
        return transform.position.x > LeftLimit.position.x && transform.position.x < RightLimit.position.x;
    }

}
