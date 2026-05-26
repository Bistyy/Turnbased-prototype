using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;
    public int health = 100;
    public int damage = 10;

    public AudioManager audioManager;
    public GameManager gameManager;
    public Slider enemySlider;
    public GameObject damageTextPrefab;

    private Renderer _renderer;

    void Start()
    {
        health = maxHealth;
        enemySlider.maxValue = maxHealth;
        enemySlider.value = health;
        _renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlashRed()
    {
        Color originalColor = _renderer.material.color;
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _renderer.material.color = originalColor;
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        audioManager.PlayAudio(audioManager.enemyHurt);
        StartCoroutine(FlashRed());
        enemySlider.value = health;
        SpawnDamageNumber(amount, false);

        if (health <= 0)
        {
            health = 0;
            gameManager.currentState = GameManager.BattleState.Win;
        }
    }

    public void SpawnDamageNumber(int amount, bool isHealing)
    {
        Vector3 spawnPosition = transform.position + Vector3.up;
        GameObject popup = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);
        popup.GetComponent<DamageNumberPopup>().Initialize(amount, isHealing);
    }

}
