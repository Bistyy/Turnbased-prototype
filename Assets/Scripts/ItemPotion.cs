using TMPro;
using UnityEngine;

public class ItemPotion : MonoBehaviour
{
    public ItemData data;
    public int currentUses;
    public TextMeshProUGUI usesText;

    private void Start()
    {
        currentUses = data.startingUses;
        UpdateUsesText();

        GetComponentInChildren<TextMeshProUGUI>().text = data.itemName; 
    }

    public void UpdateUsesText()
    {
        if (usesText == null)
        {
            Debug.LogWarning("usesText not assigned on " + gameObject.name);
            return;
        }

        usesText.text = currentUses.ToString();
    }
}
