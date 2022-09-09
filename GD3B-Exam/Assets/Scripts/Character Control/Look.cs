using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float topBound, bottomBound;

    private Vector3 _inVec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var currentRot = camAnchor.rotation.eulerAngles;

        var lookVec = currentRot - _inVec;
        var angle = lookVec.x;

        if (angle > 180 && angle < topBound)
        {
            lookVec.x = topBound;
        }
        else if (angle < 180 && angle > bottomBound)
        {
            lookVec.x = bottomBound;
        }
        
        camAnchor.rotation = Quaternion.Euler(lookVec);
    }

    private void OnLook(InputValue input)
    {
        var temp = input.Get<Vector2>();
        _inVec = new Vector3(temp.y, temp.x*-1, 0f);
    }
}
