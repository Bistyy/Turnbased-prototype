using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public GameManager gameManager;
    void Start()
    {
        
    }

    public void OnPlayerAttack()
    {
        if (gameManager.currentState == GameManager.BattleState.PlayerTurn)
        {
            enemyStats.TakeDamage(playerStats.damage);
            if (enemyStats.health > 0)
            {
                gameManager.currentState = GameManager.BattleState.EnemyTurn;
            }
        }
    }

    void OnEnemyAttack()
    {
            playerStats.TakeDamage(enemyStats.damage);
            if (playerStats.health > 0)
            {
                gameManager.currentState = GameManager.BattleState.PlayerTurn;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentState == GameManager.BattleState.EnemyTurn)
        {
            OnEnemyAttack();
        }
    }
}
