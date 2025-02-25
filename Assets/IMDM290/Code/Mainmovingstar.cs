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
        speed = 0.5f;  // Use the class variables here
        width = 40;
        height = 17;
    }

    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * Mathf.Pow(1 + Mathf.Sin(timeCounter), 3) * width; // Scaling the x-coordinate by width
        float y = 2 * Mathf.Sin(timeCounter) * height; // Scaling the y-coordinate by height
        float z = -20;

        transform.position = new Vector3(x, y);
    }
}

