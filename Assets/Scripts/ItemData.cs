using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Battle/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int minHeal;
    public int maxHeal;
    public int startingUses;

    public int GetHealAmount()
    {
        return Random.Range(minHeal, maxHeal + 1);
    }
}
