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
    {
        player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (itemtype)
            {
                case Itemtype.Heavy_Bullet:
                    
                    break;
                case Itemtype.Jump:
                    break;
                case Itemtype.XtraHealth:
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
