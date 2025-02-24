using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    float timeCounter = 0;

    float speed; // Class-level variables
    float width;
    float height;

    void Start()
    {
        speed = 0.2f;  // Use the class variables here
        width = 40;
        height = 17;
    }

    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * Mathf.Pow(1 + Mathf.Sin(timeCounter), 2) * width; // Scaling the x-coordinate by width
        float y = Mathf.Sin(timeCounter) * height; // Scaling the y-coordinate by height
        float z = 10;

        transform.position = new Vector3(x, y, z);
    }
}

