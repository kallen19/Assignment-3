using System.Collections;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    public GameObject fourstarPrefab; // Your custom fourstar prefab
    public Vector3 spawnValues; // Spawn area for random positioning
    public float spawnMostWait; // Maximum wait time for spawn
    public float spawnLeastWait; // Minimum wait time for spawn
    public int startWait; // Initial wait before spawning starts
    public bool stop; // Control spawning process

    public float rotationDuration = 9f; // Duration for rotation
    public Vector3 rotationAxis = Vector3.up; // Rotation axis

    // Color Transition Variables
    public Material material;
    public Color[] colors;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;
    public float colorTransitionTime;

    // Oscillator Parameters (Static for all spawned objects)
    public float oscAmp = 1f;  // Oscillation Amplitude (same for all objects)
    public float oscFreq = 1f; // Oscillation Frequency (same for all objects)
    private float oscPhase = 0f; // Oscillation Phase (same for all objects)

    void Start()
    {
        StartCoroutine(WaitSpawner());
    }

    void Update()
    {
        // Update color transition
        Transition();
        // Update oscillation for the spawner
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

    // Oscillation movement for the spawner (static for all spawned objects)
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
            // Random spawn position
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnValues.x, spawnValues.x),
                Random.Range(-spawnValues.y, spawnValues.y),
                Random.Range(-spawnValues.z, spawnValues.z)
            );

            // Random spawn delay
            float spawnWait = Random.Range(spawnLeastWait, spawnMostWait);

            // Instantiate the fourstar prefab and add behaviors
            GameObject spawnedObject = Instantiate(fourstarPrefab, spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);

            // Add the Rotator component to fourstar objects
            Rotator rotator = spawnedObject.AddComponent<Rotator>();
            rotator.rotationDuration = rotationDuration;
            rotator.rotationAxis = rotationAxis;

            // Add ColorTransition component for color changing effect
            ColorTransition colorTransition = spawnedObject.AddComponent<ColorTransition>();
            colorTransition.material = spawnedObject.GetComponent<Renderer>().material;
            colorTransition.colors = colors;
            colorTransition.time = colorTransitionTime;

            // Add Oscillator component for oscillation effect (with static values)
            Oscillator oscillator = spawnedObject.AddComponent<Oscillator>();
            oscillator.amp = oscAmp;
            oscillator.freq = oscFreq;
            oscillator.phase = oscPhase;

            // Wait before spawning the next fourstar
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
