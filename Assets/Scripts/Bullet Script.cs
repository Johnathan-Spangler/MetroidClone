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
    public float bulletwalldetection;
    public PlayerController player;
    public Vector3 bulletDirection;

    private float currentSpeed = 0;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletwalldetection = (GetComponent<Collider>().bounds.extents.x + 1f);
        player.GetComponent<PlayerController>();
        bulletDirection = player.moveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = speed + player.playerVelocity.x;
        ProjMove();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyScript>())
        {//lower the health of the enemy when collision with bullet
            for (int i = 0; i < bulletDamage; i++)
            {
                collision.gameObject.GetComponent<EnemyScript>().EnemyHurt();
            }
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enviorment"))
        {
            Destroy(gameObject);
        }
    }
    private void ProjMove()
    {
        Vector3 XPos = transform.position;
        XPos.x += (bulletDirection.x * currentSpeed * Time.deltaTime); //Set Absolute Value?
        transform.position = XPos;
        print("Current Speed: " + currentSpeed);
    }
}
