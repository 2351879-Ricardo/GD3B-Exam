using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO playerWeapon;
    [SerializeField] private int longestCombo;

    // Serialized purely to see in inspector.
        // Weapon Damage taken from WeaponSO and put into this variable
    [Header("Don't Change This Value")]
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponAttackSpeed;

    private GameObject _parent;
    private float _dmg;

    private void Start()
    {
        _parent = transform.root.gameObject;
        UpdateWeaponDamage();
        UpdateWeaponSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyController>().TakeDamage(_dmg);
            gameObject.SendMessageUpwards("LandHit");
        }
    }

    public void SetDamageMultiplier(float mult)
    {
        _dmg = weaponDamage * mult;
        Debug.Log(_dmg);
    }

    private void GetLongestCombo()
    {
        SendMessageUpwards("SetMaxCombo", longestCombo);
    }

    public void UpdateWeaponDamage()
    {
        weaponDamage = playerWeapon.attackDamage;
        Debug.Log("dmg"+ _dmg);
    }
    
    public void UpdateWeaponSpeed()
    {
        weaponAttackSpeed = playerWeapon.attackSpeed;
        SendMessageUpwards("SetAttackSpeed", playerWeapon.attackSpeed);
    }

    public float WeaponDamage => weaponDamage;
    public float WeaponAttackSpeed => weaponAttackSpeed;
    public WeaponSO PlayerWeapon => playerWeapon;
}
