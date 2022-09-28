using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public Transform lookPnt;

    public RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        
        var temp = new Vector3();
        var layerMask = 1 << 7;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, layerMask))
        {
            temp = hit.point;
        }

        lookPnt.position = temp;
    }
}
