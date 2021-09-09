using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    public float forceMin = 12.0f;
    public float forceMax = 16.0f;

    public float torqueAmount = 10.0f;

    public float xSpawnRange = 4;
    public float ySpawnPos = -2;

    public int pointValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        if (gameManager.IsGameActive())
        {
            gameManager.UpdateScore(pointValue);
        }
        

        if (CompareTag("Target_Bad"))
        {
            gameManager.TriggerGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (CompareTag("Target_Good"))
        {
            gameManager.TriggerGameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(forceMin, forceMax);
    }

    float RandomTorque()
    {
        return Random.Range(-torqueAmount, torqueAmount);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos, 0);
    }
}
