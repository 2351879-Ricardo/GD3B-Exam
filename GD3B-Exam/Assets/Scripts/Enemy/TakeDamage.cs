using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private EnemyController _ec;
    // Start is called before the first frame update
    void Start()
    {
        _ec = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _ec.TakeDamage(other.GetComponent<Weapon>().damage);
        }
    }
}
