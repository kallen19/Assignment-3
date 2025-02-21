using System.Collections;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    public GameObject[] ennemies; // Enemies array
    public GameObject fourstarPrefab; // Your custom fourstar prefab
    public Vector3 spawnValues; // Spawn area for random positioning
    public float spawnWait; // Random spawn wait time
    public float spawnMostWait; // Maximum wait time for spawn
    public float spawnLeastWait; // Minimum wait time for spawn
    public int startWait; // Initial wait before spawning starts
    public bool stop; // Control spawning process

    public float rotationDuration = 9f; // Duration for rotation
    public Vector3 rotationAxis = Vector3.up; // Rotation axis

    private GameObject[] fourstars; // Array to hold spawned fourstar objects
    private int randEnemy;

    // Color Transition Variables
    public Material material;
    public Color[] colors;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;
    public float colorTransitionTime;

    // Oscillator Variables
    public float oscAmp;
    public float oscFreq;
    public float oscPhase;

    void Start()
    {
        StartCoroutine(WaitSpawner());
    }

    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        // Update color transition
        Transition();
        // Update oscillation (optional: update each object's position separately)
        Oscillate();
    }

    // Color transition between materials
    void Transition()
    {
        targetPoint += Time.deltaTime / colorTransitionTime;
        material.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);

        if (targetPoint >= 1f)
        {
            targetPoint = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex++;

            if (targetColorIndex == colors.Length)
                targetColorIndex = 0;
        }
    }

    // Oscillation movement for the spawners or objects
    void Oscillate()
    {
        oscPhase += Time.deltaTime * oscFreq;
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            Mathf.Sin(oscPhase) * oscAmp + transform.localPosition.y,
            transform.localPosition.z
        );

        if (oscPhase > Mathf.PI * 2)
        {
            oscPhase -= Mathf.PI * 2;
        }
    }

    IEnumerator WaitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            randEnemy = Random.Range(0, ennemies.Length + 1); // Could spawn either enemies or fourstars

            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnValues.x, spawnValues.x),
                Random.Range(-spawnValues.y, spawnValues.y),
                Random.Range(-spawnValues.z, spawnValues.z)
            );

            GameObject spawnedObject = null;

            if (randEnemy < ennemies.Length)
            {
                spawnedObject = Instantiate(ennemies[randEnemy], spawnPosition, Quaternion.identity);
            }
            else
            {
                // Instantiate the fourstar prefab and add behaviors
                spawnedObject = Instantiate(fourstarPrefab, spawnPosition, Quaternion.identity);
                // Add the Rotator component to fourstar objects
                Rotator rotator = spawnedObject.AddComponent<Rotator>();
                rotator.rotationDuration = rotationDuration;
                rotator.rotationAxis = rotationAxis;

                // Add ColorTransition component for color changing effect
                ColorTransition colorTransition = spawnedObject.AddComponent<ColorTransition>();
                colorTransition.material = spawnedObject.GetComponent<Renderer>().material;
                colorTransition.colors = colors;
                colorTransition.time = colorTransitionTime;

                // Add Oscillator component for oscillation effect
                Oscillator oscillator = spawnedObject.AddComponent<Oscillator>();
                oscillator.amp = oscAmp;
                oscillator.freq = oscFreq;
                oscillator.phase = oscPhase;
            }

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
