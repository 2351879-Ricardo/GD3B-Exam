using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private float sensitivityX, sensitivityY;
    [SerializeField] private float topAngle, bottomAngle;

    private float _angleX, _angleY, _camTilt;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, _angleX * Time.deltaTime);

        _camTilt -= _angleY*Time.deltaTime;
        _camTilt = Mathf.Clamp(_camTilt, bottomAngle, topAngle);
        var targetRot = transform.eulerAngles;
        targetRot.x = _camTilt;
        playerCam.eulerAngles = targetRot;

    }

    private void OnLook(InputValue input)
    {
        var temp = input.Get<Vector2>();
        //temp = temp.normalized;
        _angleX = temp.x * sensitivityX;
        _angleY = temp.y * sensitivityY;
    }

    /*private void OnLookY(InputValue input)
    {
        var temp = input.Get<float>();
        _angleY = temp * sensitivityY;

    }

    private void OnLookX(InputValue input)
    {
        var temp = input.Get<float>();
        _angleX = temp*sensitivityX;
    }*/
}
