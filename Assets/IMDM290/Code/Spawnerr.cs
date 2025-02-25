using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

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

    public int numDivisions = 2;
    public static float[] amplitudes;
    
    int currentDivision = 0;
    public float yOffset = 2f;

    public float scale = 10f;
    
 public List<float> multipliers = new List<float>();
 int randEnemy;

 List <GameObject> star;

 void Start ()
 {
  StartCoroutine(waitSpawner());

  source = GetComponent<AudioSource>(); 

  star = new List<GameObject>();
  
  amplitudes = new float[numDivisions];

 }
 
 void Update ()
 {
  for(int i = 0; i < amplitudes.Length; i++)
  {
   amplitudes[i] = 0;
  }
  
  spawnWait = Random.Range (spawnLeastWait, spawnMostWait);

  // The source (time domain) transforms into samples in frequency domain 
        GetComponent<AudioSource>().GetSpectrumData(samples, 0, FFTWindow.Hanning);
        // Empty first, and pull down the value.
        audioAmp = 0f;
        
        for (int i = 0; i < FFTSIZE; i++)
        {
         amplitudes[i * numDivisions / FFTSIZE] += samples[i] * multipliers[i * numDivisions / FFTSIZE];
        }

        int whichDivision = 0;
        for (int i = star.Count - 1; i >= 0; i--)
        {
         star[i].transform.localScale = 2 * new Vector3(amplitudes[whichDivision],amplitudes[whichDivision],amplitudes[whichDivision]);
         if (whichDivision == numDivisions - 1)
         {
          whichDivision = 0;
         }
         else
         {
          whichDivision++;
         }
        }
 }

 IEnumerator waitSpawner()
 {
  yield return new WaitForSeconds (startWait);

  while (!stop)
  {
   Debug.Log(currentDivision);
   randEnemy = Random.Range (0, 2);

   Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y) + currentDivision * yOffset, Random.Range (-spawnValues.z, spawnValues.z));

   GameObject objs = Instantiate (ennemies[currentDivision], spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);

   objs.AddComponent<EndMyselfPrematurely>();

   star.Add(objs);
   
   
   if (currentDivision == numDivisions - 1)
   {
    currentDivision = 0;
   }
   else
   {
    currentDivision++;

   yield return new WaitForSeconds (spawnWait);
  }

  }
 }
}