using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

/* Johnathan Spangler
 * 10/28/25
 * Controls the player
 */

public class PlayerController : MonoBehaviour
{
    public bool moving = false;
    public bool jumping = false;
    public float speed = 10;
    public int playerLives = 99;
    public Vector3 moveDirection = Vector3.right;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector3.right;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector3.left;
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            moveDirection = Vector3.up;
            jumping = true;
        }
        else
        {
            jumping = false;
        }
    }

    private void FixedUpdate()
    {
        if (moving == true)
        {
            print("Moving " + moveDirection);
            rb.AddForce(moveDirection * (speed * 100) * Time.deltaTime, ForceMode.Force);
        }
        if (jumping == true)
        {
            print("Moving " + moveDirection);
            rb.AddForce(moveDirection * (speed * 10) * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
