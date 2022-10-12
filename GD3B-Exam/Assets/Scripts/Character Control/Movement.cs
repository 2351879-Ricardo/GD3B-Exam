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
    private CharacterState _cs;
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cs = GetComponent<CharacterState>();
        _anim = GetComponent<Animator>();
    }

    //Runs per set amount of time.
    private void FixedUpdate()
    {
        #region StateControl
        
        #endregion
        //Sets vertical velocity when _jumping is true
        #region Jump
        //physics check to see if player is on ground
        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer);

        if (_isGrounded)
        {
            _verticalV = Vector3.zero;
        }
        
        //if the player is jumping the vertical velocity
        //  is set so the player is able to jump to a specified height.
        if (_jumping)
        {
            if (_isGrounded)
            {
                _verticalV.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
            }
            _jumping = false;
        }
        #endregion
        //Sets horizontal velocity when _dodging is true.
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
            if (_dodging)
            {
                SendMessage("Dodging");
            }
            _dodging = false;
            
        }
        _anim.SetBool("dodging", _dodging);
        #endregion
        //Uses _moveVec to set movement direction in tandem with horizontal and vertical velocity.
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

    //Listens for Message from New Input System
    //On Activation, manipulates the _moveVec variable to give the player a movement direction.
    private void OnMove(InputValue value)
    {
        var temp = value.Get<Vector2>();
        Debug.Log($"{temp.normalized}");
        _inVec = temp.normalized;
    }
    
    //Listens for message from New Input System.
    //Sets _jumping boolean to true on activation.
    private void OnJump(InputValue value)
    {
        _jumping = true;
    }

    //Listens for message from New Input System.
    //Sets _jumping boolean to true on activation.
    private void OnDodge()
    {
        _dodging = true;
        _dodgeTimeR = dodgeTime;
        SendMessage("Dodging");
    }
}
