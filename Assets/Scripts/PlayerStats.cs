using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int maxHealth = 150;
    public int health = 150;
    public int damage = 15;
    public GameManager gameManager;
    public Slider playerSlider;
    void Start()
    {
        health = maxHealth;
        playerSlider.maxValue = maxHealth;
        playerSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        playerSlider.value = health;
        if (health <= 0)
        {
            health = 0;
            gameManager.currentState = GameManager.BattleState.Lose;
        }
    }
}
