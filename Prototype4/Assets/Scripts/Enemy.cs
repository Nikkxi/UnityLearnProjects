using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRb;

    public float movementSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 travelVector = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(travelVector * movementSpeed);

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
