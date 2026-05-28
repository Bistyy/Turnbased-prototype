using TMPro;
using UnityEngine;

public class ItemPotion : MonoBehaviour
{
    public int healAmount;
    public int minHeal;
    public int maxHeal;
    public int uses;
    public TextMeshProUGUI usesText;

    private void Start()
    {
        UpdateUsesText();
    }

    public int GetHealAmount()
    {
        return Random.Range(minHeal, maxHeal + 1);
    }
    public void UpdateUsesText()
    {
        if (usesText == null)
        {
            Debug.LogWarning("usesText not assigned on " + gameObject.name);
            return;
        }

        usesText.text = uses.ToString();
    }
}
