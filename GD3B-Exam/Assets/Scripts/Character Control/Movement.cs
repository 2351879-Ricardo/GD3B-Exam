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
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float jumpHeight, gravity;
    [SerializeField] private float dodgeDistance, dodgeTime;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody _rb;
    private Vector2 _inVec;
    private Vector3 _verticalV = Vector3.zero;
    private Vector3 _dogdeV = Vector3.zero;
    private bool _jumping, _dodging, _isGrounded;
    private float _dodgeTimeR;
    
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
        #region Jump
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
        #endregion

        #region Dodge
        if (!_dodging)
        {
            _dogdeV = Vector3.zero;
        }
        else if (_dodging && moveVec == Vector3.zero)
        {
            _dogdeV = camAnchor.forward * (dodgeDistance/dodgeTime)*-1;
            _dodgeTimeR -= Time.deltaTime;
        }
        else if (_dodging)
        {
            _dogdeV = _rb.velocity.normalized * (dodgeDistance/dodgeTime);
            _dodgeTimeR -= Time.deltaTime;
        }

        if (_dodgeTimeR <= 0)
        {
            _dodging = false;
        }
        #endregion
        
        #region MoveAround
        moveVec = new Vector3(_inVec.x, 0f, _inVec.y);
        
        var camFwd = camAnchor.forward;
        var camRt = camAnchor.right;

        camFwd.y = 0;
        camRt.y = 0;

        var dir = (camFwd * moveVec.z + camRt * moveVec.x);

        _verticalV.y += gravity * Time.deltaTime;
        var totalV = dir * moveSpeed + _verticalV + _dogdeV;

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

    private void OnDodge()
    {
        _dodging = true;
        _dodgeTimeR = dodgeTime;
    }
}
