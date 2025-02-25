using UnityEngine;

public class EndMyselfPrematurely : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;
    
    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0f)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}