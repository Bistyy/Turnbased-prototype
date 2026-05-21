using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public GameManager gameManager;
    private bool isProcessing; // interestingly enough, booleans set their default value to false..
    private bool isBattleOver;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    void Start()
    {
        
    }
    IEnumerator EnemyTurnRoutine()
    {
        isProcessing = true;
        yield return new WaitForSeconds(1f);
        OnEnemyAttack();
        isProcessing = false; 
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
        if (gameManager.currentState == GameManager.BattleState.EnemyTurn && !isProcessing)
        {
            StartCoroutine(EnemyTurnRoutine());
        }

        if (gameManager.currentState == GameManager.BattleState.Win && !isBattleOver)
        {
            resultPanel.SetActive(true);
            resultText.text = "You Win!";
            isBattleOver = true;
        }

        else if (gameManager.currentState == GameManager.BattleState.Lose && !isBattleOver)
        {
            resultPanel.SetActive(true);
            resultText.text = "You Lose!";
            isBattleOver = true;
        }
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
