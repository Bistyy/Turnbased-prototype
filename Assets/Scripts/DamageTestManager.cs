using UnityEngine;

public class DamageTestManager : MonoBehaviour
{
    private const int MinDamage = 5;
    private const int MaxDamage = 50;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryClickOnDamageable();
        }
    }

    private void TryClickOnDamageable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Damageable damageable = hit.collider.GetComponent<Damageable>();

            if (damageable != null)
            {
                int damage = Random.Range(MinDamage, MaxDamage + 1);
                damageable.TakeDamage(damage);
            }
        }
    }
}
