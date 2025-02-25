using UnityEngine;

public class lifeTime : MonoBehaviour
{
    public float lifetime;

    void Start()
    {
        Destroy (gameObject, lifetime);
    }
}
