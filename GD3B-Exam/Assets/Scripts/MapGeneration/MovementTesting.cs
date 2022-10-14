using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTesting : MonoBehaviour
{
    public CharacterController controller;
    public Transform player;

    float WalkSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Input.GetAxis("Horizontal");
        float zPos = Input.GetAxis("Vertical");
        Vector3 PlayerMove = (transform.right * xPos) + (transform.forward * zPos);
        controller.Move(PlayerMove * WalkSpeed * Time.deltaTime);
    }
}
