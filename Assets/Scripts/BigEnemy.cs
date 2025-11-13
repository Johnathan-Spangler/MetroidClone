using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/*
 * Topher Overbey
 * 10/28/2025
 * Controls the movement and the damage that the Big enemy can take
 */

public class BigEnemy : MonoBehaviour
{
    public Transform Player1;
    public float speed = 3f;
    public int Health;
    public float dist2Player;
    public float detectionRange = 5f;
    public bool doMoveRight = false;


    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1)
        {
            // the second point is the position of the MonoBehaviour's transform
            dist2Player = Vector3.Distance(Player1.position, transform.position);
        }
        IsPlayerRight();
        MoveBig();
        
    }
    public void EnemyHurt()
    {
        Health--;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void MoveBig()
    {

        transform.position += direction * speed * Time.deltaTime;

        if (dist2Player <= detectionRange)
        {
                if (doMoveRight == false)
                {
                    direction = Vector3.left;
                }

        }
        if (dist2Player <= detectionRange)
        {
                if (doMoveRight == true)
                {
                    direction = Vector3.right;
                }
        }
        
    }
    private void IsPlayerRight()
    {


        {
            if (Player1.transform.position.x >= gameObject.transform.position.x)
            {
                doMoveRight=true;
            }
            else
            {
                doMoveRight=false;
            }
        }
    }
}




