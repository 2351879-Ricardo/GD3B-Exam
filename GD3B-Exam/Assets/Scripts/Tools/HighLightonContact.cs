using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightonContact : MonoBehaviour
{
    private Material _mat;
    [SerializeField] private float activatedRadius;
    [SerializeField] private float deactivatedRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<Renderer>().material;
        _mat.SetFloat("_OutlineWidth", deactivatedRadius);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _mat.SetFloat("_OutlineWidth", activatedRadius);
    }

    private void OnTriggerExit(Collider other)
    {
        _mat.SetFloat("_OutlineWidth", deactivatedRadius);
    }
}
