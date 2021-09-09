using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;

    public float movementSpeed = 5.0f;

    public float xBounds = 10.0f;
    public float zBounds = 10.0f;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == false)
        {
            keepPlayerInBounds();

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isGameOver = true;
            collision.gameObject.GetComponent<EnemyController>().movementSpeed = 0;
            Debug.Log("Game Over!");
        }
    }

    void keepPlayerInBounds()
    {
        // Check +/- X bounds, stop the player, then give the player a small bounce in the other direction
        if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
        }

        // Check +/- Z bounds
        if (transform.position.z > zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBounds);
        }
        if (transform.position.z < -zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBounds);
        }
    }
}
