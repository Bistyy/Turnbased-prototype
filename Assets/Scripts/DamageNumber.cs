using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1.5f;
    [SerializeField] private float duration = 1f;

    private TextMeshPro _text;
    private float _elapsed;
    private Color _startColor;
    private Camera _mainCamera;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        _startColor = _text.color;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        transform.LookAt(_mainCamera.transform.position);

        float alpha = Mathf.Lerp(1f, 0f, _elapsed / duration);
        _text.color = new Color(_startColor.r, _startColor.g, _startColor.b, alpha);

        if (_elapsed >= duration)
            Destroy(gameObject);
    }

}
