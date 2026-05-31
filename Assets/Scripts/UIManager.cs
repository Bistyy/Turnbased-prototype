using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject mainMenu;
    public GameObject skillsMenu;
    public GameObject itemsMenu;
    public GameObject resultPanel;

    public TextMeshProUGUI actionDescText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI turnText;

    private void OnEnable()
    {
        gameManager.OnStateChanged += HandleStateChanged;
    }

    void HandleStateChanged(GameManager.BattleState newState)
    {
        switch (newState)
        {
            case GameManager.BattleState.PlayerTurn:                
                {
                    ShowPanel(mainMenu);
                    turnText.text = "Player Turn";
                }
                break;

            case GameManager.BattleState.EnemyTurn:
                {
                    HidePanel();
                    turnText.text = "Enemy Turn!";                   
                }
                break;

            case GameManager.BattleState.Win:
                {
                    HidePanel();
                    resultPanel.SetActive(true);
                    resultText.text = "YOU WIN!!!";
                    turnText.gameObject.SetActive(false);
                }
                break;

            case GameManager.BattleState.Lose:
                {
                    HidePanel();
                    resultPanel.SetActive(true);
                    resultText.text = "YOU LOSE!!";
                    turnText.gameObject.SetActive(false);
                }
                break;
        }
    }
    public void ShowPanel(GameObject panel)
    {
        mainMenu.SetActive(false);
        skillsMenu.SetActive(false);
        itemsMenu.SetActive(false);

        panel.SetActive(true);
        SetDescription("");

    }

    public void HidePanel()
    {
        mainMenu.SetActive(false);
        skillsMenu.SetActive(false);
        itemsMenu.SetActive(false);

        SetDescription("");

    }

    public void OnBackPressed()
    {
        ShowPanel(mainMenu);
    }

    public void OnSkillsPressed()
    {
        ShowPanel(skillsMenu);
    }

    public void OnItemsPressed()
    {
        ShowPanel(itemsMenu);
    }

    public void SetDescription(string text)
    {
        actionDescText.text = text;
    }
}
