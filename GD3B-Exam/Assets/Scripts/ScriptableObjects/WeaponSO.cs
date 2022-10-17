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
    public string itemName;
    [TextArea(5, 15)] public string itemDescription;
    [SerializeField] private Sprite itemSprite;

    [Header("Weapon Information")] 
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public WeaponType weaponType;
    public DamageType damageType;

    // Getters
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemSprite => itemSprite;
}