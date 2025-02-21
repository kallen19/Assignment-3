using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

 public GameObject [] ennemies;
 public Vector3 spawnValues;
 public float spawnWait;
 public float spawnMostWait;
 public float spawnLeastWait;
 public int startWait;
 public bool stop;

    AudioSource source;
    public static int FFTSIZE = 1024; // https://en.wikipedia.org/wiki/Fast_Fourier_transform
    public static float[] samples = new float[FFTSIZE];
    public static float audioAmp = 0f;

 int randEnemy;

 List <GameObject> star;

 void Start ()
 {
  StartCoroutine(waitSpawner());

  source = GetComponent<AudioSource>(); 

  star = new List<GameObject>();
 }
 
 void Update ()
 {
  spawnWait = Random.Range (spawnLeastWait, spawnMostWait);

  // The source (time domain) transforms into samples in frequency domain 
        GetComponent<AudioSource>().GetSpectrumData(samples, 0, FFTWindow.Hanning);
        // Empty first, and pull down the value.
        audioAmp = 0f;
        for (int i = 0; i < FFTSIZE; i++)
        {
            audioAmp += samples[i];
        }        

    foreach(GameObject obj in star)
    {
        obj.transform.localScale = new Vector3 (audioAmp,audioAmp, audioAmp);
    }
 }

 IEnumerator waitSpawner()
 {
  yield return new WaitForSeconds (startWait);

  while (!stop)
  {
   randEnemy = Random.Range (0, 2);

   Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), Random.Range (-spawnValues.z, spawnValues.z));

   GameObject objs = Instantiate (ennemies[randEnemy], spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);

   star.Add(objs);

   yield return new WaitForSeconds (spawnWait);
  }
 }
}
