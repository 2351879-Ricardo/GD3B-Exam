using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDamage : MonoBehaviour
{
    private float _dmg;
    public bool _targetHit;

    private Transform _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.root;
        _dmg = _parent.GetComponent<EnemyController>().EnemySo.EnemyDamagePerAttack;
        _dmg *= _parent.GetComponent<EnemyController>().EnemySo.ChargeDamageMult;
        _targetHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !_targetHit)
        {
            _targetHit = true;
            Debug.Log($"Hit     {other.gameObject.name}");
            //EndCharge();
            var temp = other.gameObject.GetComponentInParent<PlayerStats>();
            temp.TakeDamage(_dmg);
            temp.KnockBack(_parent, 100);
        }
    }

    private void EndCharge()
    {
        GetComponent<Collider>().enabled = false;
        _targetHit = false;
    }
}
