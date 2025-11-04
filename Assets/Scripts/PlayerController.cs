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
    public bool bulletUpgrade = false;
    public bool jumpUpgrade = false;
    //public bool ballUpgrade = false;
    public float speed = 10;
    public float jPower = 10;
    public int playerLives = 99;
    public Vector3 moveDirection = Vector3.right;

    public GameObject bullet1;
    public GameObject bullet2;

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
        Shooting();
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
            //print("Moving " + moveDirection);
            rb.AddForce(moveDirection * (speed * jPower) * Time.deltaTime, ForceMode.Impulse);
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

        if (jumpUpgrade)
        {
            jPower = 20;
        }
    }

    private void OnGround()
    {
        RaycastHit hit;

        Vector3 OffsetX = transform.position;
        Vector3 OffsetY = transform.position;

        OffsetX.x += (GetComponent<Collider>().bounds.extents.x + 0.25f); // + 0.25 for cayote time
        OffsetY.y -= (GetComponent<Collider>().bounds.extents.y + 0.25f); // + 0.25 for cayote time

        if ((Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity) && (hit.point.y >= OffsetY.y) && hit.collider.CompareTag("Enviorment"))
            || (Physics.Raycast(OffsetX, Vector3.down, out hit, Mathf.Infinity) && (hit.point.y >= OffsetY.y) && hit.collider.CompareTag("Enviorment")) 
            || (Physics.Raycast(-OffsetX, Vector3.down, out hit, Mathf.Infinity) && (hit.point.y >= OffsetY.y) && hit.collider.CompareTag("Enviorment"))
            )
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Period)) {
            Vector3 bulletPos = transform.position;
            float Offset = (GetComponent<Collider>().bounds.extents.x + 0.5f);
            if (moveDirection.x == 1)
            {
                Offset *= -1;
            }
            else if (moveDirection.x == -1)
            {
                Offset *= 1;
            }
            bulletPos.x += transform.position.x + Offset;
            if (!bulletUpgrade)
            {
                Instantiate(bullet1, bulletPos, transform.rotation);
            }
            if (bulletUpgrade)
            {
                Instantiate(bullet2, bulletPos, transform.rotation);
            }
        }
    }
}
