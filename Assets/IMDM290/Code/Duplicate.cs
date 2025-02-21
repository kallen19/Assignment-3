using UnityEngine;

public class Duplicate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update() {
	if (Input.GetKeyDown(KeyCode.A)) {
		Instantiate(gameObject);
	}
}
}
