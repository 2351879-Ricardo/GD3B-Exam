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
        #region MoveAround
        Debug.Log($"{_inVec.x}      {_inVec.y}");
        moveVec = new Vector3(_inVec.x, 0f, _inVec.y);
        
        var camFwd = camAnchor.forward;
        var camRt = camAnchor.right;

        camFwd.y = 0;
        camRt.y = 0;

        var dir = (camFwd * moveVec.z + camRt * moveVec.x);

        _rb.velocity = dir * moveSpeed;
        #endregion
    }

    private void OnMove(InputValue value)
    {
        var temp = value.Get<Vector2>();
        _inVec = temp.normalized;
    }
}
