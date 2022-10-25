using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoors : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("TestPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player)
        {
            Debug.Log("Entered door");
            //Call script to move player to arena
        }
    }
}
