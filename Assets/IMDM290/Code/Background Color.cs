using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    //code by: Into The 3D -https://www.youtube.com/watch?v=gaFbwu1e0rI
    [SerializeField] private Camera cameraRef;
    [SerializeField] private Color[] colors;
    [SerializeField] private float colorChangeSpeed;
    [SerializeField] private float time;
    private float currentTime;
    private int colorIndex;

    [SerializeField] private float timeDelay;

    private void Awake()
    {
        cameraRef = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = time + timeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        colorChange();
        colorChangeTime();
    }

    private void colorChange()
    {
        cameraRef.backgroundColor = Color.Lerp(cameraRef.backgroundColor, colors[colorIndex], colorChangeSpeed * Time.deltaTime);
    }

    private void colorChangeTime()
    {
        if (currentTime <= 0)
        {
            colorIndex++;
            CheckColorIndex();
            currentTime = time;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void CheckColorIndex()
    {
        if (colorIndex >= colors.Length)
        {
            colorIndex = 0;
        }
    }
    private void OnDestroy()
    {
        cameraRef.backgroundColor = colors[0];
    }
}
