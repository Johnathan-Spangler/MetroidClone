using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 10/30/2025
 * Controls the Damage that things to to players
 */

public class EnemyDamage : MonoBehaviour
{
    public int damage = 15;
    public PlayerController dealDamage;
    // Start is called before the first frame update
    void Start()
    {//get the player script to use later
        dealDamage.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DamagePower()
    {//deal damage to the player's lives based on the damage variable
        dealDamage.playerLives -= damage;
    }
    public void OnTriggerEnter(Collider other)
    {//when we collide with soemthing, if it is the player damage it
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePower();
        }

    }
}
