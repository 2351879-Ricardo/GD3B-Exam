using UnityEngine;

public enum WeaponType
{
    MELEE,
    RANGED,
    MAGIC,
    SHIELD
};

public enum DamageType
{
    SLASH,
    BLUNT,
    PIERCE
};

[CreateAssetMenu(menuName = "SO's/Weapons", fileName = "NewWeapon")]
public class WeaponSO : ScriptableObject
{
    [Header("General Information")]
    [SerializeField] private string itemName;
    [TextArea(5, 15)] [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemSprite;

    [Header("Weapon Information")] 
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private DamageType damageType;

    // Getters
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemSprite => itemSprite;
    
    public float AttackDamage => attackDamage;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public WeaponType WeaponType => weaponType;
    public DamageType DamageType => damageType;
}