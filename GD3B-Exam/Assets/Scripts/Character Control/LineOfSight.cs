using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public Transform lookPnt;

    public RaycastHit hit;

    [SerializeField] private float interactDistance;
    // Start is called before the first frame update
    void Start()
    {
        hit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 temp;
        var layerMask = 1 << 7;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, layerMask))
        {
            temp = hit.point;
        }
        
        else
        {
            temp = (transform.forward * interactDistance) + transform.position;
        }

        lookPnt.position = temp;
    }
}
