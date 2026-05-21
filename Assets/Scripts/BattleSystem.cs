using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
    public TextMeshProUGUI turnText;
    public Button attackButton;
    void Start()
    {
        turnText.text = "Player Turn";
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
            attackButton.gameObject.SetActive(false);
            turnText.text = "Enemy Turn";
        }
    }

    void OnEnemyAttack()
    {
        playerStats.TakeDamage(enemyStats.damage);
        if (playerStats.health > 0)
        {
            gameManager.currentState = GameManager.BattleState.PlayerTurn;
        }
        attackButton.gameObject.SetActive(true);
        turnText.text = "Player Turn";
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
            attackButton.gameObject.SetActive(false);
            resultText.text = "You Win!";
            isBattleOver = true;
            turnText.gameObject.SetActive(false);
        }

        else if (gameManager.currentState == GameManager.BattleState.Lose && !isBattleOver)
        {
            resultPanel.SetActive(true);
            attackButton.gameObject.SetActive(false);
            resultText.text = "You Lose!";
            isBattleOver = true;
            turnText.gameObject.SetActive(false);
        }
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
