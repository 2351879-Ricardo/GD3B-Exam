using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float rotationSpeed,topBound, bottomBound;

    private Vector3 _inVec;

    private float _lookX, _lookY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void Update()
    {
        var currentRot = camAnchor.localRotation.eulerAngles;

        _lookX = Mathf.Clamp(_lookX + _inVec.x * (rotationSpeed*0.1f), topBound, bottomBound);
        _lookY += _inVec.y * (rotationSpeed*0.1f);

        var lookVec = new Vector3(_lookX*-1, _lookY, 0f);
        
        camAnchor.localRotation = Quaternion.Euler(lookVec);
    }

    private void OnLook(InputValue input)
    {
        var temp = input.Get<Vector2>();
        _inVec = new Vector3(temp.y, temp.x*-1, 0f);
    }
}
