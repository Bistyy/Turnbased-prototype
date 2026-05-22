using UnityEngine;
using TMPro;

public class DamageNumberPopup : MonoBehaviour
{
    private const float FloatSpeed = 1.5f;
    private const float Lifetime = 1f;

    private TextMeshPro _text;
    private float _timer;

    /// <summary>
    /// Sets the damage value displayed by this popup.
    /// </summary>
    public void Initialize(int damageAmount)
    {
        _text = GetComponent<TextMeshPro>();
        _text.text = "-" + damageAmount;
        _timer = 0f;
    }

    private void Update()
    {
        // Float upward over time
        transform.position += Vector3.up * FloatSpeed * Time.deltaTime;

        // Always face the camera
        transform.forward = Camera.main.transform.forward;

        // Count up and destroy when lifetime is reached
        _timer += Time.deltaTime;
        if (_timer >= Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
