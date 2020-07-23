using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowPlayer : MonoBehaviour
{
    Transform player;
    float minX, maxX;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        minX = 0;
        maxX = 105;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 temp = transform.position;
            temp.x = player.position.x;
            if (temp.x < minX) {
                temp.x = minX;
            }
            if(temp.x > maxX)
            {
                temp.x = maxX;
            }
            transform.position = temp;
        }
    }
}
