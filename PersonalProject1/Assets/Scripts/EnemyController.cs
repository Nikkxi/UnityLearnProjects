using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float bounds = 20.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move forward
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

        DestroyIfOutOfBounds();
    }

    void DestroyIfOutOfBounds()
    {
        Vector3 currentPos = transform.position;

        if (currentPos.x < -bounds || currentPos.x > bounds || currentPos.z < -bounds || currentPos.z > bounds)
        {
            Destroy(gameObject);
        }
    }
}
