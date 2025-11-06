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
    public PlayerController player;
    public bool bulletdirection;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletdirection = true)
        {
            direction = Vector3.right;
        }
        if (bulletdirection = false)
        {
            direction = Vector3.left;
        }
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
