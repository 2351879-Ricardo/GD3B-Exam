using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    //public Transform Player;
    public GameObject Player;
    float xPlayerPos;
    float zPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("TestPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        xPlayerPos = Player.transform.position.x;
        zPlayerPos = Player.transform.position.z;
        if ((Mathf.Abs(gameObject.transform.position.x - xPlayerPos) > 55) || (Mathf.Abs(gameObject.transform.position.z - zPlayerPos) > 55))
        {
            Destroy(gameObject);
        }
    }
}
