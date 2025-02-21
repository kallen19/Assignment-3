using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float amp;
    public float freq;
    public float phase;
    Vector3 intiPos;

    const float TWOPI = 2 * Mathf.PI;

    void Start()
    {
        intiPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        phase += Time.deltaTime * freq; //were something is in the sine wave

       transform.localPosition = new Vector3(intiPos.x, Mathf.Sin(phase) * amp + intiPos.y, intiPos.z); //Calculates the new position

       //Prevents phase from getting to big
       if(phase > TWOPI) 
       {
        phase = phase - TWOPI;
       }
    }
}