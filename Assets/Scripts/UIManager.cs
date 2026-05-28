using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject skillsMenu;
    public GameObject itemsMenu;

    public TextMeshProUGUI actionDescText;
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
