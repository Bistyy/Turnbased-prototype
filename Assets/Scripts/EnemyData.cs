using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Battle/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int minNormalDamage;
    public int maxNormalDamage;
    public int minHeavyDamage;
    public int maxHeavyDamage;
    public int normalAttackWeight;
    public int heavyAttackWeight;
    public int tauntWeight;
}
