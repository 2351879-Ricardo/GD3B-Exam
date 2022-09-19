using UnityEngine;

public enum EnemyType
{
    RANGE,
    MELEE,
    MAGIC
}

[CreateAssetMenu(menuName = "SO's/Enemies", fileName = "NewEnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private float enemyAttackSpeed = 1f;
    [SerializeField] private float enemyDamagePerAttack = 5f;
    [SerializeField] private float enemyAttackRange = 10f;
    [SerializeField] private float enemyNoticeRange = 20f;
    [SerializeField] private EnemyType enemyType;
    
    public float EnemyHealth
    {
        get => enemyHealth;
        set => enemyHealth = value;
    }
    
    public float EnemyAttackSpeed
    {
        get => enemyAttackSpeed;
        set => enemyAttackSpeed = value;
    }
    
    public float EnemyDamagePerAttack
    {
        get => enemyDamagePerAttack;
        set => enemyDamagePerAttack = value;
    }
    
    public float EnemyAttackRange
    {
        get => enemyAttackRange;
        set => enemyAttackRange = value;
    }

    public float EnemyNoticeRange
    {
        get => enemyNoticeRange;
        set => enemyNoticeRange = value;
    }

    public EnemyType EnemyType => enemyType;
}
