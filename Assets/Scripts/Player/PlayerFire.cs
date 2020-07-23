using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireBall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(fireBall, firePoint.position, firePoint.rotation);
    }
}
