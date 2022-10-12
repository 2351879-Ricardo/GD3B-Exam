using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum DamageType
    {
        RANGE,
        BLUNT,
        MAGIC
    };

    public DamageType damageType;
    
    public float damage;

    private GameObject _parent;
    private float _dmg;

    private void Start()
    {
        _parent = transform.root.gameObject;
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
        _dmg = damage * mult;
        Debug.Log(_dmg);
    }
}
