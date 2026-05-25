using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Hierarchy;

public class BattleSystem : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public GameManager gameManager;
    public CameraShake cameraShake;

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

    IEnumerator PlayerAttackRoutine()
    {
        Vector3 originalPlayerPos = playerStats.transform.position;
        Vector3 originalEnemyPos = enemyStats.transform.position;

        float elapsed = 0f;
        float duration = 0.3f;
        float offset = 2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration; // why are we doing this?
            playerStats.transform.position = Vector3.Lerp(originalPlayerPos, originalEnemyPos + Vector3.left * offset, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        elapsed = 0f;

        // damage
        enemyStats.TakeDamage(playerStats.damage);
        StartCoroutine(cameraShake.Shake());

        // go back to starting point
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration; // why are we doing this?
            playerStats.transform.position = Vector3.Lerp(originalEnemyPos + Vector3.left * offset, originalPlayerPos, t);
            yield return null;
        }
        // enemy turn after reaching starting point?
        if (enemyStats.health > 0)
        {
            gameManager.currentState = GameManager.BattleState.EnemyTurn;
        }
    }

    public void OnPlayerAttack()
    {
        if (gameManager.currentState == GameManager.BattleState.PlayerTurn)
        {
            StartCoroutine(PlayerAttackRoutine());

            attackButton.gameObject.SetActive(false);
            turnText.text = "Enemy Turn";
        }
    }

    IEnumerator EnemyAttackRoutine()
    {
        Vector3 originalEnemyPosition = enemyStats.transform.position;
        Vector3 originalPlayerPosition = playerStats.transform.position;

        float elapsed = 0f;
        float duration = 0.3f;
        float offset = 2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            enemyStats.transform.position = Vector3.Lerp(originalEnemyPosition, originalPlayerPosition + Vector3.right * offset, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        playerStats.TakeDamage(enemyStats.damage);
        StartCoroutine(cameraShake.Shake());
        elapsed = 0f;

        // go back
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            enemyStats.transform.position = Vector3.Lerp(originalPlayerPosition + Vector3.right * offset, originalEnemyPosition, t);
            yield return null;
        }

        isProcessing = false;

        // if player isnt dead, make it their turn
        if (playerStats.health > 0)
        {
            gameManager.currentState = GameManager.BattleState.PlayerTurn;
            attackButton.gameObject.SetActive(true);
            turnText.text = "Player Turn";
        }
    }

    void OnEnemyAttack()
    {
        StartCoroutine(EnemyAttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentState == GameManager.BattleState.EnemyTurn && !isProcessing)
        {
            isProcessing = true;
            StartCoroutine(EnemyAttackRoutine());
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
