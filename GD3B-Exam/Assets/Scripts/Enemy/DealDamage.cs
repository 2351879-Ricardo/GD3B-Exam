using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private float _dmg;

    private void Start()
    {
        _dmg = transform.root.gameObject.GetComponent<EnemyController>().EnemySo.EnemyDamagePerAttack;
        Debug.Log(_dmg);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Punch Thrown");
        if (other.CompareTag("Player"))
        {
            SendMessageUpwards("DealDamage", _dmg);
        }
    }
}