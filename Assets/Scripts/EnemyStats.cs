using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;
    public int health = 100;
    public int damage = 10;
    public GameManager gameManager;
    public Slider enemySlider;
    void Start()
    {
        health = maxHealth;
        enemySlider.maxValue = maxHealth;
        enemySlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        enemySlider.value = health;
        if (health <= 0)
        {
            health = 0;
            gameManager.currentState = GameManager.BattleState.Win;
        }
    }
}
