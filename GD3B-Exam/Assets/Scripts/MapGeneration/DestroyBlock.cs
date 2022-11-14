using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    //public Transform Player;
    private GameObject _player;
    private float _xPlayerPos;
    private float _zPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerStats>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _xPlayerPos = _player.transform.position.x;
        _zPlayerPos = _player.transform.position.z;
        if ((Mathf.Abs(gameObject.transform.position.x - _xPlayerPos) > 55) || (Mathf.Abs(gameObject.transform.position.z - _zPlayerPos) > 55))
        {
            Destroy(gameObject);
        }
    }
}
