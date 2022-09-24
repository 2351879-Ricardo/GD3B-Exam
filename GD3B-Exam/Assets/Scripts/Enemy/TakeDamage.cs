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

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Weapon")
        {
            Debug.Log("Hit");
            _ec.TakeDamage(other.collider.GetComponent<Weapon>().damage);
        }
    }
}
