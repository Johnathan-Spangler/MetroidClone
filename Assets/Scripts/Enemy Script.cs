using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 10/28/2025
 * Controls the movement and the damage that an enemy can take
 */

public class EnemyScript : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 3f;
    public int Health;
  

    private Vector3 direction;
    private Vector3 startLeftPos;
    private Vector3 startRightPos;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.left;
        startLeftPos = leftPoint.position;
        startRightPos = rightPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
        //check if enemy is moveing left and is at the start left posistion
        if (direction == Vector3.left && transform.position.x <= startLeftPos.x)
        {//change direction to right
            direction = Vector3.right;
        }
        else if (direction == Vector3.right && transform.position.x >= startRightPos.x)
        {
            direction = Vector3.left;
        }
    }
    public void EnemyHurt()
    {
        Health--;
        if (Health <= 0)
        {
           Destroy(gameObject);
        }
    }
}
