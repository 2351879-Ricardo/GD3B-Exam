using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;


public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 moveVec;
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float moveSpeed, jumpHeight, gravity;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody _rb;
    private Vector2 _inVec;
    private Vector3 _verticalV = Vector3.zero;
    private bool _jumping, _isGrounded;
    
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
        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer);

        if (_isGrounded)
        {
            _verticalV = Vector3.zero;
        }

        if (_jumping)
        {
            if (_isGrounded)
            {
                _verticalV.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
            }
            _jumping = false;
        }
        
        #region MoveAround
        moveVec = new Vector3(_inVec.x, 0f, _inVec.y);
        
        var camFwd = camAnchor.forward;
        var camRt = camAnchor.right;

        camFwd.y = 0;
        camRt.y = 0;

        var dir = (camFwd * moveVec.z + camRt * moveVec.x);

        _verticalV.y += gravity * Time.deltaTime;
        var totalV = dir * moveSpeed + _verticalV;

        _rb.velocity = totalV;
        #endregion
    }

    private void OnMove(InputValue value)
    {
        var temp = value.Get<Vector2>();
        _inVec = temp.normalized;
    }

    private void OnJump(InputValue value)
    {
        _jumping = true;
    }
}
