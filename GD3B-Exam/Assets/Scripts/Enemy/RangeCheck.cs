using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RangeCheck : MonoBehaviour
{
    public float chargeRange, meleeRange, checkTime;
    [Range(0, 360)] public float fieldOfView = 90f;
    public bool charge, melee, view;

    private Transform _player;

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Rad2Deg));
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

        var directionToTarget = _player.position - transform.position;
        directionToTarget = directionToTarget.normalized;
        if (Vector3.Angle(transform.forward, directionToTarget) < fieldOfView / 2)
        {
            view = true;
        }
        else
            view = false;
    }

    public float CheckTime => checkTime;
    public bool InView => view;
}
