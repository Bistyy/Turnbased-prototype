using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public int maxHealth = 150;
    public int skillUses = 3;
    public int currentHealth;
    public int minNormalDamage = 7;
    public int maxNormalDamage = 13;
    public int minHeavyDamage = 22;
    public int maxHeavyDamage = 33;

    public AudioManager audioManager;
    public GameManager gameManager;
    public Slider playerSlider;

    public GameObject damageTextPrefab;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI spText;

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
        DamageNumberPopup.SpawnDamageNumber(damageTextPrefab,transform.position,amount, false);

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
        DamageNumberPopup.SpawnDamageNumber(damageTextPrefab, transform.position, amount, true);
        audioManager.PlayAudio(audioManager.itemHeal);
        StartCoroutine(Flash(Color.green));
    }

    public int GetNormalDamage()
    {
        return Random.Range(minNormalDamage, maxNormalDamage + 1);
    }

    public int GetHeavyDamage()
    {
        return Random.Range(minHeavyDamage, maxHeavyDamage + 1);
    }

}
