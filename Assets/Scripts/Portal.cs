using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 10/12/2025
 * Controls power of the portal and where it sends people
*/

public class Portal : MonoBehaviour
{
    public Transform teleportPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    private void OnTriggerEnter(Collider other)
    {//sets the touched object's pos to the tp's pos
        other.transform.position = teleportPoint.position;
    }
}
