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
        
        var temp = new Vector3();
        var layerMask = 1 << 7;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, layerMask))
        {
            Debug.Log(hit.collider.name);
            temp = hit.point;
            
        }
        else
        {
            temp = (transform.forward * interactDistance) + transform.position;
        }

        lookPnt.position = temp;
    }
}
