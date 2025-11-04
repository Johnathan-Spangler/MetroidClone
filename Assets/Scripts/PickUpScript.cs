using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 11/4/2025
 * Controls the items you pick up
*/

public class PickUpScript : MonoBehaviour
{
    public int itemtype = 0;
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
            if (itemtype == 0)
            {

            }
            if (itemtype == 1)
            {

            }
            if (itemtype == 2)
            {

            }
            if (itemtype == 3)
            {
                player.playerLives += healpower;
            }

            Destroy(gameObject);
        }
    }
   
}
