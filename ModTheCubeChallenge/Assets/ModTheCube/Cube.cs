using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    // for bouncing scale
    private float count = 0;
    private bool increasing = true;
    public float scaleBounceDuration = 1.0f;

    // for changing color
    private Material material;
    public float currentRedValue;
    private bool increasingColor = true;

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * Random.Range(1.3f, 3.0f);
        
        material = Renderer.material;

        currentRedValue = Random.Range(0.0f, 1.0f);
        material.color = new Color(currentRedValue, Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.4f, 1.0f));
    }
    
    void Update()
    {
        // bounce the scale of the cube over time
        if (increasing)
        {
            count += Time.deltaTime;
            transform.localScale += Vector3.one * Time.deltaTime;
            if (count > scaleBounceDuration)
                increasing = false;
        }
        else
        {
            count -= Time.deltaTime;
            transform.localScale -= Vector3.one * Time.deltaTime;
            if (count < 0)
                increasing = true;
        }


        // shift the Red channel of the cube
        if(increasingColor)
        {
            currentRedValue += 0.01f;
            material.color = new Color(currentRedValue, material.color.g, material.color.b, material.color.a);
            if(currentRedValue > 1)
            {
                increasingColor = false;
            }
        }
        else
        {
            currentRedValue -= 0.01f;
            material.color = new Color(currentRedValue, material.color.g, material.color.b, material.color.a);
            if (currentRedValue < 0)
            {
                increasingColor = true;
            }
        }
        material.color = new Color(currentRedValue, material.color.g, material.color.b, material.color.a);


        // default transform from the base project version
        transform.Rotate(30.0f * Time.deltaTime, 15.0f * Time.deltaTime, 5.0f * Time.deltaTime);
    }
}
