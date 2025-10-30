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
    {
        dealDamage.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DamagePower()
    {
        dealDamage.playerLives -= damage;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePower();
        }

    }
}
