using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 
 * 
 * 
*/

public class PickUpScript : MonoBehaviour
{
    public int itemtype = 0;
    // Start is called before the first frame update
    void Start()
    {

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

            }

            Destroy(gameObject);
        }
    }
}
