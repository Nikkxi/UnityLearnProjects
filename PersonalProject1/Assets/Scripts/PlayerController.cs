using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameManager gameManager;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 15.0f;

    public float xBounds = 10.0f;
    public float zBounds = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive == true)
        {
            keepPlayerInBounds();

            float forwardInput = Input.GetAxis("Vertical");
            float rotationInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.forward * movementSpeed * forwardInput * Time.deltaTime);
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();
            Destroy(collision.gameObject);
            //Destroy(gameObject);
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
