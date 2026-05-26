using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 150;
    public int health = 150;
    public int damage = 15;
    public int skillUses = 3;
    public int itemUses = 3;

    public AudioManager audioManager;
    public GameManager gameManager;
    public Slider playerSlider;

    public GameObject damageTextPrefab;

    private Animator _animator;
    private Renderer _renderer;
    void Start()
    {
        health = maxHealth;
        playerSlider.maxValue = maxHealth;
        playerSlider.value = health;

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
        yield return new WaitForSeconds(0.2f);
        _renderer.material.color = originalColor;
    }

    public void TriggerAnimation(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        audioManager.PlayAudio(audioManager.playerHurt);
        StartCoroutine(Flash(Color.red));
        playerSlider.value = health;
        SpawnDamageNumber(amount, false);

        if (health <= 0)
        {
            health = 0;
            TriggerAnimation("Death");
            gameManager.currentState = GameManager.BattleState.Lose;
        }
        else
        {
            TriggerAnimation("HitReaction");
        } 
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        playerSlider.value = health;
        SpawnDamageNumber(amount, true);
        audioManager.PlayAudio(audioManager.itemHeal);
        StartCoroutine(Flash(Color.green));
    }
    public void SpawnDamageNumber(int amount, bool isHealing)
    {
        Vector3 spawnPosition = transform.position + Vector3.up;
        GameObject popup = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);
        popup.GetComponent<DamageNumberPopup>().Initialize(amount, isHealing);
    }

}
