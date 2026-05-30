using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public EnemyData data;
    public int currentHealth;

    public AudioManager audioManager;
    public GameManager gameManager;
    public Slider enemySlider;

    public GameObject damageTextPrefab;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI nameText;

    private Renderer _renderer;

    void Start()
    {
        currentHealth = data.maxHealth;
        enemySlider.maxValue = data.maxHealth;
        enemySlider.value = currentHealth;
        hpText.text = currentHealth.ToString();
        nameText.text = data.enemyName;
        _renderer = GetComponent<Renderer>();

    }
    public enum EnemyAction
    {
        NormalAttack,
        HeavyAttack,
        DoNothing
    }

    public EnemyAction DecideAction()
    {
        float totalWeight = data.normalAttackWeight + data.heavyAttackWeight + data.tauntWeight;
        float roll = Random.Range(0, totalWeight);

        if (roll < data.normalAttackWeight)
        {
            return EnemyAction.NormalAttack;
        }
        else if (roll < data.normalAttackWeight + data.heavyAttackWeight)
        {
            return EnemyAction.HeavyAttack;
        }
        else
        {
            return EnemyAction.DoNothing;
        }
    }

    public int GetNormalDamage()
    {
        return Random.Range(7, 13 + 1);
    }

    public int GetHeavyDamage()
    {
        return Random.Range(19, 28 + 1);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, data.maxHealth);

        audioManager.PlayAudio(audioManager.enemyHurt);
        StartCoroutine(FlashRed());
        enemySlider.value = currentHealth;
        hpText.text = currentHealth.ToString();
        SpawnDamageNumber(amount, false);

        if (currentHealth <= 0)
        {
            gameManager.currentState = GameManager.BattleState.Win;
        }
    }
    IEnumerator FlashRed()
    {
        Color originalColor = _renderer.material.color;
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _renderer.material.color = originalColor;
    }

    public void SpawnDamageNumber(int amount, bool isHealing)
    {
        Vector3 spawnPosition = transform.position + Vector3.up;
        GameObject popup = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);
        popup.GetComponent<DamageNumberPopup>().Initialize(amount, isHealing);
    }

}
