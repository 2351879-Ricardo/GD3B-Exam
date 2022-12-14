using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 moveVec;
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float jumpHeight, gravity;
    [SerializeField] private float dodgeDistance, dodgeTime, dodgeCoolDownTime, stepTime;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody _rb;
    private Vector2 _inVec;
    private Vector3 _verticalV = Vector3.zero;
    private Vector3 _dogdeV = Vector3.zero;
    private Vector3 _stepV = Vector3.zero;
    private bool _jumping, _dodging, _canDodge, _isGrounded, _stepping;
    private float _dodgeTimeR, _dodgeCoolDown, _stepTime, _stepTimeR;
    private CharacterState _cs;
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cs = GetComponent<CharacterState>();
        _anim = GetComponent<Animator>();
        _canDodge = true;
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

        if (!_canDodge)
        {
            _dodgeCoolDown -= Time.deltaTime;
        }

        if (_dodgeCoolDown <= 0f)
        {
            _canDodge = true;
        }
        
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
                _canDodge = false;
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
        #endregion
        //Step Forward Control - Used in tandem with animation event

        #region Step Forward

        if (_stepping)
        {
            _stepTime -= Time.deltaTime;
            _stepV = 0.5f * moveSpeed * camFwd;
            _stepV.y = 0;
        }

        if (_stepTime <= 0)
        {
            _stepping = false;
            _stepV = Vector3.zero;
        }
        #endregion
        var totalV = dir * moveSpeed + _verticalV + _dogdeV + _stepV;

        _rb.velocity = totalV;
    }

    //Listens for Message from New Input System
    //On Activation, manipulates the _moveVec variable to give the player a movement direction.
    private void OnMove(InputValue value)
    {
        var temp = value.Get<Vector2>();
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
        if (_canDodge)
        {
            _dodging = true;
            _canDodge = false;
            _dodgeCoolDown = dodgeCoolDownTime + dodgeTime;
            _dodgeTimeR = dodgeTime;
            SendMessage("Dodging");
        }
    }

    public void StepForward()
    {
        _stepping = true;
        _stepTime = stepTime;
    }
}
