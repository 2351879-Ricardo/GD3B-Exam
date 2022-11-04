using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangeCheck : MonoBehaviour
{
    public float chargeRange, meleeRange, checkTime;
    public bool charge, melee;

    private Transform _player;
    private void OnDrawGizmos()
    {
        var pos = transform.position;
        Handles.DrawWireDisc(pos, Vector3.up, chargeRange);
        Handles.DrawWireDisc(pos, Vector3.up, meleeRange);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerStats>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(transform.position, _player.position);
        if (dist < meleeRange)
        {
            melee = true;
        }
        else
        {
            melee = false;
        }

        if (dist < chargeRange)
        {
            charge = true;
        }
        else
        {
            charge = false;
        }
    }

    public float CheckTime => checkTime;
    
}
