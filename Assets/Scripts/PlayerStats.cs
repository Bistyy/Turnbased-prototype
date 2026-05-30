using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public int maxHealth = 150;
    public int skillUses = 3;
    public int currentHealth = 150;

    public AudioManager audioManager;
    public GameManager gameManager;
    public Slider playerSlider;

    public GameObject damageTextPrefab;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI spText;
    public TextMeshProUGUI itemText;

    private Animator _animator;
    private Renderer _renderer;
    void Start()
    {
        currentHealth = maxHealth;
        playerSlider.maxValue = maxHealth;
        playerSlider.value = currentHealth;
        hpText.text = currentHealth.ToString();
        spText.text = skillUses.ToString();
        _renderer = GetComponentInChildren<Renderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Flash(Color flashColor)
    {
        Color originalColor = _renderer.material.color;
        _renderer.material.color = flashColor;
        yield return new WaitForSeconds(0.3f);
        _renderer.material.color = originalColor;
    }

    public void TriggerAnimation(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        audioManager.PlayAudio(audioManager.playerHurt);
        StartCoroutine(Flash(Color.red));
        playerSlider.value = currentHealth;
        hpText.text = currentHealth.ToString();
        SpawnDamageNumber(amount, false);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            TriggerAnimation("Death");
            gameManager.currentState = GameManager.BattleState.Lose;
        }
        else
        {
            TriggerAnimation("HitReaction");
        } 
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        playerSlider.value = currentHealth;
        hpText.text = currentHealth.ToString();
        SpawnDamageNumber(amount, true);
        audioManager.PlayAudio(audioManager.itemHeal);
        StartCoroutine(Flash(Color.green));
    }

    public int GetNormalDamage()
    {
        return Random.Range(7, 13 + 1);
    }

    public int GetHeavyDamage()
    {
        return Random.Range(22, 33 + 1);
    }

    public void SpawnDamageNumber(int amount, bool isHealing)
    {
        Vector3 spawnPosition = transform.position + Vector3.up;
        GameObject popup = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);
        popup.GetComponent<DamageNumberPopup>().Initialize(amount, isHealing);
    }

}
