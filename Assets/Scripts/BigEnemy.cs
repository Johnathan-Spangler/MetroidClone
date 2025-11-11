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
    public float dist2Left;
    public float dist2Right;
    public float detectionRange = 5f;
    public bool doMove = false;
    public Transform leftPoint;
    public Transform rightPoint;


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
        }
        if (leftPoint)
        {
            // the second point is the position of the MonoBehaviour's transform
            dist2Left = Vector3.Distance(leftPoint.position, transform.position);
        }
        if (rightPoint)
        {
            // the second point is the position of the MonoBehaviour's transform
            dist2Right = Vector3.Distance(rightPoint.position, transform.position);
        }
        MoveBig();
    }
    private void MoveBig()
    {

        transform.position += direction * speed * Time.deltaTime;

        if (dist2Player <= detectionRange)
        {
            if (dist2Player >= dist2Left)
            {
                direction = Vector3.left;
            }
        }
        else if (dist2Player >= detectionRange)
        {
            if (dist2Player <= dist2Right)
            {
                direction = Vector3.right;
            }
        }
        else
        {
            direction = Vector3.zero;
        }
    }

}




