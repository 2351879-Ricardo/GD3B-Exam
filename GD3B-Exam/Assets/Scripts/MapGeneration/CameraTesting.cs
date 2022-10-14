using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public Transform Player;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xMousePos = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float yMousePos = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation -= yMousePos;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * xMousePos);
    }
}
