using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    RANGE,
    MELEE,
    MAGIC
}

[System.Serializable]
public struct EnemyLootDrop
{
    [SerializeField] private float lootDropChance;
    [SerializeField] private int lootDropAmount;
    [SerializeField] private ResourceSO resourceDrop;
    [SerializeField] private GameObject resourcePrefab;

    public float LootDropChance => lootDropChance;
    public int LootDropAmount => lootDropAmount;
    public ResourceSO ResourceDrop => resourceDrop;
    public GameObject ResourcePrefab => resourcePrefab;
};

[CreateAssetMenu(menuName = "SO's/Enemies", fileName = "NewEnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float enemyHealth = 100f;
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float enemyAttackSpeed = 1f;
    [SerializeField] private float enemyDamagePerAttack = 5f;
    [SerializeField] private float enemyAttackRange = 10f;
    [SerializeField] private float enemyHitChance01 = 0.8f;
    
    [Header("Attack Probabilities")] 
    [SerializeField] private float chargeChance;
    [SerializeField] private float rangeChance;
    
    [SerializeField] private EnemyType enemyType;

    [SerializeField] private List<EnemyLootDrop> enemyDropTable;
    
    public float EnemyHealth
    {
        get => enemyHealth;
        set => enemyHealth = value;
    }

    public float EnemySpeed
    {
        get => enemySpeed;
        set => enemySpeed = value;
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

    public float EnemyHitChance01
    {
        get => enemyHitChance01;
        set => enemyHitChance01 = value;
    }

    public EnemyType EnemyType => enemyType;
    public List<EnemyLootDrop> EnemyDropTable => enemyDropTable;
}
