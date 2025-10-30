using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 10/28/2025
 * Control the power of a Bullet when it collides with a enemy
 */

public class BulletScript : MonoBehaviour
{
    public int bulletDamage = 1;
    public float speed = 3f;
    public float bulletwalldetection = 1;
    public int directionint;
    
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProjMove();
        BulletWall();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyScript>())
        {//lower the health of the enemy when collision with bullet
            for (int i = 0; i < bulletDamage; i++)
            {
                collision.gameObject.GetComponent<EnemyScript>().EnemyHurt();
            }
        }
        Destroy(gameObject);
    }
    private void ProjMove()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (directionint == 1)
        {//change direction to right
            direction = Vector3.right;
        }
        else if (directionint == 0)
        {
            direction = Vector3.left;
        }
    }
    private void BulletWall()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, bulletwalldetection) && CompareTag("Enviorment"))
        {
            Destroy(gameObject);
        }
    }
}
