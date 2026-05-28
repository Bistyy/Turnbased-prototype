using System.Collections;
using TMPro;
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
    public TextMeshProUGUI hpText;

    private Renderer _renderer;

    [SerializeField] private float normalAttackWeight = 60f;
    [SerializeField] private float heavyAttackWeight = 30f;
    [SerializeField] private float tauntWeight = 10f;

    void Start()
    {
        health = maxHealth;
        enemySlider.maxValue = maxHealth;
        enemySlider.value = health;
        hpText.text = health.ToString();
        _renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum EnemyAction
    {
        NormalAttack,
        HeavyAttack,
        DoNothing
    }

    public EnemyAction DecideAction()
    {
        float totalWeight = normalAttackWeight + heavyAttackWeight + tauntWeight;
        float roll = Random.Range(0, totalWeight);

        if (roll < normalAttackWeight)
        {
            return EnemyAction.NormalAttack;
        }
        else if (roll < normalAttackWeight + heavyAttackWeight)
        {
            return EnemyAction.HeavyAttack;
        }
        else
        {
            return EnemyAction.DoNothing;
        }
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
        hpText.text = health.ToString();
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
