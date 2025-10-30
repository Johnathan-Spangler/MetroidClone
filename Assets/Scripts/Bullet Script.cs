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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyScript>())
        {//lower the health of the enemy when collision with bullet
            collision.gameObject.GetComponent<EnemyScript>().EnemyHurt();
        }
    }
}
