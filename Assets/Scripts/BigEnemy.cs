using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 10/28/2025
 * Controls the movement and the damage that the Big enemy can take
 */

public class BigEnemy : MonoBehaviour
{
    public Transform other;
    public float speed = 3f;
    public int Health;
    public float dist2Player;
    public float detectionRange = 5f;


    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (other)
        {
            // the second point is the position of the MonoBehaviour's transform
            dist2Player = Vector3.Distance(other.position, transform.position);
            //print("Distance to other: " + dist2Player);
        }
        
        
        MoveBig();
        
        
    }
    private void MoveBig()
    {
        if (dist2Player == detectionRange)
        {
            transform.position += direction * speed * Time.deltaTime;
            if (dist2Player <= detectionRange)
            {
                direction = Vector3.left;
            }
            if (dist2Player >= detectionRange)
            {
                direction = Vector3.right;
            }
        }
        
    }
}




