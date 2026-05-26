using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Camera _camera;
    float duration = 0.2f;
    float magnitude = 0.07f;
    public IEnumerator Shake()
    {
        float elapsed = 0f;

        Vector3 originalCameraPos = transform.position;

        // shake cam
        while (elapsed < duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);

            elapsed += Time.deltaTime;
            transform.position = originalCameraPos + new Vector3(x, y, 0);
            yield return null;
        }

        transform.position = originalCameraPos;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
