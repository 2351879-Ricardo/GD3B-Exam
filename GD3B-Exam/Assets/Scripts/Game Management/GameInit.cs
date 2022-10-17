using System;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    [SerializeField] private WeaponSO baseWeapon;
    [SerializeField] private WeaponSO playerWeapon;
    
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        ResetWeapon();
    }

    private void ResetWeapon()
    {
        playerWeapon.attackDamage = baseWeapon.attackDamage;
        playerWeapon.attackRange = baseWeapon.attackRange;
        playerWeapon.attackSpeed = baseWeapon.attackSpeed;
    }
}
