using UnityEngine;

public class lifeTime : MonoBehaviour
{
    public float lifetime;

    void Update()
    {
        Destroy (gameObject, lifetime);
    }
}
