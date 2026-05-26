using UnityEngine;

public class Damageable : MonoBehaviour
{
    private const float PopupHeightOffset = 0.0f;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject damageNumberPrefab;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    /// <summary>
    /// Applies damage to this entity and spawns a damage number popup above it.
    /// </summary>
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        SpawnDamageNumber(amount);

        Debug.Log($"{gameObject.name} took {amount} damage. HP: {_currentHealth}/{maxHealth}");
    }

    private void SpawnDamageNumber(int amount)
    {
        Vector3 spawnPosition = transform.position + Vector3.up * PopupHeightOffset;
        GameObject popup = Instantiate(damageNumberPrefab, spawnPosition, Quaternion.identity);
    }
}
