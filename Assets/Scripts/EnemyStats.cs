using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health = 100;
    public int damage = 10;
    public GameManager gameManager;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            health = 0;
            gameManager.currentState = GameManager.BattleState.Win;
        }
    }
}
