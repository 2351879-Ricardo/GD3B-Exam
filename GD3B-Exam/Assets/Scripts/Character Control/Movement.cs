using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 moveVec;

    [SerializeField] private float moveSpeed;
    private Rigidbody _rb;
    private Vector2 _inVec;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = moveVec * moveSpeed;
    }

    private void OnMove(InputValue value)
    {
        var temp = value.Get<Vector2>();
        _inVec = temp.normalized;
        moveVec = new Vector3(_inVec.x, 0f, _inVec.y);
    }
}
