using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 150;
    public int health = 150;
    public int damage = 15;
    public GameManager gameManager;
    public Slider playerSlider;

    public GameObject damageTextPrefab;

    private Renderer _renderer;
    void Start()
    {
        health = maxHealth;
        playerSlider.maxValue = maxHealth;
        playerSlider.value = health;

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
        StartCoroutine(FlashRed());
        playerSlider.value = health;
        SpawnDamageNumber(amount);

        if (health <= 0)
        {
            health = 0;
            gameManager.currentState = GameManager.BattleState.Lose;
        }
    }

    private void SpawnDamageNumber(int amount)
    {
        Vector3 spawnPosition = transform.position + Vector3.up;
        GameObject popup = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);
        popup.GetComponent<DamageNumberPopup>().Initialize(amount);
    }

}
