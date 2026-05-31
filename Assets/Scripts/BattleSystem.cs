using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public CameraShake cameraShake;

    private IDamageable _enemyTarget;
    private IDamageable _playerTarget;

    public GameManager gameManager;
    public AudioManager audioManager;
    public UIManager uiManager;

    public TextMeshProUGUI turnText;

    private bool isProcessing; // interestingly enough, booleans set their default value to false..
    private bool isBattleOver;
    void Start()
    {
        _enemyTarget = enemyStats;
        _playerTarget = playerStats;
    }

    private void OnEnable()
    {

        gameManager.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        gameManager.OnStateChanged -= HandleStateChanged;
    }

    void HandleStateChanged(GameManager.BattleState newState)
    {
        switch (newState)
        {
            case GameManager.BattleState.EnemyTurn:
                if (!isProcessing)
                {
                    isProcessing = true;
                    StartCoroutine(EnemyAttackRoutine());
                }
                break;
        }
    }
    IEnumerator PlayerAttackRoutine(int damage, string animationTrigger)
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
        playerStats.TriggerAnimation(animationTrigger);
        yield return new WaitForSeconds(0.765f);

        elapsed = 0f;

        audioManager.PlayAudio(audioManager.playerHit);
        _enemyTarget.TakeDamage(damage);
        StartCoroutine(cameraShake.Shake());

        yield return new WaitForSeconds(0.765f);

        // go back to starting point
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration; // why are we doing this?
            playerStats.transform.position = Vector3.Lerp(originalEnemyPos + Vector3.left * offset, originalPlayerPos, t);
            yield return null;
        }
        // enemy turn after reaching starting point?
        if (enemyStats.GetCurrentHealth() > 0)
        {
            gameManager.CurrentState = GameManager.BattleState.EnemyTurn;
        }
    }

    IEnumerator PlayerItemRoutine(int healAmount)
    {
        yield return new WaitForSeconds(1.6f);
        playerStats.Heal(healAmount);
        yield return new WaitForSeconds(0.8f);
        gameManager.CurrentState = GameManager.BattleState.EnemyTurn;

    }
    public void OnPlayerAttack()
    {
        if (gameManager.CurrentState == GameManager.BattleState.PlayerTurn)
        {
            uiManager.HidePanel();
            StartCoroutine(PlayerAttackRoutine(playerStats.GetNormalDamage(), "Attack"));
        }
    }

    public void OnSkillAttack()
    {
        if (gameManager.CurrentState == GameManager.BattleState.PlayerTurn && playerStats.HasSkillUses())
        {
            uiManager.HidePanel();
            playerStats.UseSkill();
            StartCoroutine(PlayerAttackRoutine(playerStats.GetHeavyDamage(), "Skill"));
        }
    }

    public void OnItemUse(ItemPotion item)
    {
        if (gameManager.CurrentState == GameManager.BattleState.PlayerTurn && item.currentUses > 0)
        {
            item.currentUses -= 1;
            item.UpdateUsesText();
            playerStats.TriggerAnimation("UseItem");
            uiManager.HidePanel();
            StartCoroutine(PlayerItemRoutine(item.data.GetHealAmount()));
        }   
    }
    IEnumerator EnemyAttackRoutine()
    {

        EnemyStats.EnemyAction action = enemyStats.DecideAction();

        int damageToDeal = 0;

        if (action == EnemyStats.EnemyAction.DoNothing)
        {
            turnText.text = "Enemy does Nothing!";
            yield return new WaitForSeconds(1f);
            gameManager.CurrentState = GameManager.BattleState.PlayerTurn;
            uiManager.ShowPanel(uiManager.mainMenu);
            isProcessing = false;
            yield break;
        }

        else if (action == EnemyStats.EnemyAction.NormalAttack)
        {
            turnText.text = "Enemy attacks!";
            damageToDeal = enemyStats.GetNormalDamage();
        }
        else if (action == EnemyStats.EnemyAction.HeavyAttack)
        {
            turnText.text = "Enemy uses skill!";
            damageToDeal = enemyStats.GetHeavyDamage();
        }

        // movement code
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

        audioManager.PlayAudio(audioManager.enemyHit);

        yield return new WaitForSeconds(0.1f);

        _playerTarget.TakeDamage(damageToDeal);
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
        if (playerStats.GetCurrentHealth() > 0)
        {
            gameManager.CurrentState = GameManager.BattleState.PlayerTurn;
        }
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
