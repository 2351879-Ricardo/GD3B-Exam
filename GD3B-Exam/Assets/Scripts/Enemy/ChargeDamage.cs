using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDamage : MonoBehaviour
{
    private float _dmg;

    private Transform _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.root;
        _dmg = _parent.GetComponent<EnemyController>().EnemySo.EnemyDamagePerAttack;
        _dmg *= _parent.GetComponent<EnemyController>().EnemySo.ChargeDamageMult;
        Debug.Log($"Charge Damage:  {_dmg}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerStats>().TakeDamage(_dmg);
        }
    }

    private void EndCharge()
    {
        GetComponent<SphereCollider>().enabled = false;
    }
}
