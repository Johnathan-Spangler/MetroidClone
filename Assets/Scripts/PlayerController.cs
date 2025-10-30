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
    public bool grounded = false;
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
        OnGround();
        Movement();
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

    private void Movement()
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

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && grounded == true)
        {
            moveDirection = Vector3.up;
            jumping = true;
        }
        else
        {
            jumping = false;
        }
    }

    private void OnGround()
    {
        RaycastHit hit;

        Vector3 Offset = transform.position;

        Offset.x += (GetComponent<Collider>().bounds.extents.x + 0.25f); // + 0.25 for cayote time
        Offset.y += (GetComponent<Collider>().bounds.extents.y + 0.25f); // + 0.25 for cayote time

        if ((Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity) && (hit.distance <= Offset.y) && hit.collider.CompareTag("Enviorment"))
            && (Physics.Raycast(Offset, Vector3.down, out hit, Mathf.Infinity) && (hit.distance <= Offset.y) && hit.collider.CompareTag("Enviorment")) 
            && (Physics.Raycast(-Offset, Vector3.down, out hit, Mathf.Infinity) && (hit.distance <= Offset.y) && hit.collider.CompareTag("Enviorment")) //FIX OFFSET VARIABLES, THEY HAVE THE Y TOO, DON'T NEED THAT
            )
        {
            grounded = true;
        }
    }
}
