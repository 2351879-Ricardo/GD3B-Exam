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

    private void Start()
    {
        _parent = transform.root.gameObject;
        Debug.Log(_parent.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
            gameObject.SendMessageUpwards("LandHit");
        }
    }
}