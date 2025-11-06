using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 11/4/2025
 * Controls the items you pick up
*/
public enum Itemtype
{ 
    Heavy_Bullet, Jump, XtraHealth, Heal
}

public class PickUpScript : MonoBehaviour
{
    public Itemtype itemtype = Itemtype.Heal;
    public PlayerController player;
    public int healpower = 20;

    // Start is called before the first frame update
    void Start()
    {//get the player's script
        player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {//if the player is collided with, look at the item type then use it's script
        if (other.gameObject.CompareTag("Player"))
        {
            switch (itemtype)
            {
                case Itemtype.Heavy_Bullet:
                    player.bulletUpgrade = true;
                    break;
                case Itemtype.Jump:
                    player.jumpUpgrade = true;
                    break;
                case Itemtype.XtraHealth:
                    player.playerLives += 100;
                    break;
                case Itemtype.Heal:
                    player.playerLives += healpower;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
   
}
