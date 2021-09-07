using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    public float energyGainRate = 1.0f;
    public float energy = 0;

    // Update is called once per frame
    void Update()
    {
        energy += Time.deltaTime * energyGainRate;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && energy > 2.0f)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            energy = 0;
        }
    }
}
