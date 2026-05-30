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
    public void Initialize(int damageAmount, bool isHealing)
    {
        _text = GetComponent<TextMeshPro>();
        transform.localScale = Vector3.one * 0.4f;
        if (isHealing)
        {
            _text.text = "+" + damageAmount;
            _text.color = Color.green;
        }
        else
        {
            _text.text = "-" + damageAmount;
            _text.color = Color.white;
        }
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

    public static void SpawnDamageNumber(GameObject prefab, Vector3 position, int amount, bool isHealing)
    {
        GameObject popup = Instantiate(prefab, position + Vector3.up, Quaternion.identity);
        popup.GetComponent<DamageNumberPopup>().Initialize(amount, isHealing);
    }
}
