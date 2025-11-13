using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/* Johnathan Spangler
 * 10/28/25
 * Controls the player
 */

public class PlayerController : MonoBehaviour
{
    public bool moving = false, jumping = false, grounded = false, shot = false, jumpUpgrade = false, bulletUpgrade = false;//, ballUpgrade = false;

    public int playerLives = 99;
    public float speed = 10, jPower = 10;
    public int sceneIndex;

    public Vector3 playerVelocity = Vector3.zero, moveDirection = Vector3.right, currentDirection = Vector3.zero;

    public GameObject bullet1, bullet2;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity.x = Mathf.Abs(rb.velocity.x); //Used to update bullet velocity with player momentum
        OnGround();
        Movement();
        if (playerLives <= 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyDamage>())
        {
            collision.gameObject.GetComponent<EnemyDamage>().DamagePower();
        }
    }
    private void FixedUpdate()
    {
        if (moving == true)
        {
            rb.AddForce(moveDirection * (speed * 100) * Time.deltaTime, ForceMode.Force);
        }
        if (jumping == true)
        {
            currentDirection.y = moveDirection.y;
            rb.AddForce(currentDirection * (jPower * 400) * Time.deltaTime, ForceMode.Force);
        }
    }

    /// <summary>
    /// Controll player movement and shooting logic
    /// </summary>
    private void Movement()
    {
        if (((moveDirection == Vector3.left && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))) || (moveDirection == Vector3.right && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))) && grounded)
        {
            //Modify player velocity to prevent a lot of sliding when attempting to move in the opposite direction
            playerVelocity.x /= 3;
            if (rb.velocity.x < 0)//Account for negative velocity, because for some reason that's something that exists?? Even though velocity is speed, which SHOULD be direction independent.. But whatever..
            {
                playerVelocity.x *= -1;
            }
            rb.velocity = playerVelocity;
            //print("Ground Back Move");
            //print("Velocity1: " + rb.velocity.x);
        }
        else if (((moveDirection == Vector3.left && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))) || (moveDirection == Vector3.right && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))) && !grounded)
        {
            //Modify player velocity to slow velocity when attempting to move in the opposite direction while airborne
            playerVelocity.x /= 2;
            if (rb.velocity.x < 0)
            {
                playerVelocity.x *= -1;
            }
            rb.velocity = playerVelocity;
            //print("Air Back Move");
            //print("Velocity2: " + rb.velocity.x);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector3.right;
            moving = true;
            //print("Right");
            //print("Velocity3: " + rb.velocity.x);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector3.left;
            moving = true;
            //print("Left");
            //print("Velocity4: " + rb.velocity.x);
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

        if ((Input.GetKey(KeyCode.Period) || Input.GetKey(KeyCode.KeypadPeriod)) && !shot)
        {
            StartCoroutine(Timer());
        }
    }

    /// <summary>
    /// Check if player is touching the ground, implementing cayote time to make it less frustrating to control
    /// </summary>
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

    /// <summary>
    /// Limit player shooting with a timer
    /// </summary>
    /// <returns></returns>
    public IEnumerator Timer()
    {
        shot = true;
        Shooting();
        yield return new WaitForSeconds(0.5f);
        shot = false;
    }

    /// <summary>
    /// Spawn projectile, swapping to big bullet when the upgrade is collected
    /// </summary>
    public void Shooting()
    {
        if (!shot)//Make sure timer's done before proceeding
        {
            return;
        }
        Vector3 bulletPos = transform.position;//Move bullet to the correct side depending on facing direction
        float Offset = (GetComponent<Collider>().bounds.extents.x + 0.5f);
        if (moveDirection.x == 1)
        {
            Offset *= 1;
        }
        else if (moveDirection.x == -1)
        {
            Offset *= -1;
        }
        bulletPos.x += Offset;
        if (!bulletUpgrade)//Spawn normal bullet
        {
            GameObject newBullet = Instantiate(bullet1, bulletPos, transform.rotation);
            newBullet.GetComponent<BulletScript>().player = this;
        }
        else//Spawn big bullet
        {
            Quaternion rotOffset = transform.rotation;
            rotOffset.z = 0.7071068f;// I used a calculator to find this value. It makes the bullet rotate at 90 deg from the player. Here's the calculator: https://www.andre-gaschler.com/rotationconverter
            rotOffset.w = 0.7071068f;// Apparently I need this too? Idk what it does, but the calculator said it existed, and it doesn't work without it, so..

            GameObject newBullet = Instantiate(bullet2, bulletPos, rotOffset);
            newBullet.GetComponent<BulletScript>().player = this;
        }
    }
}