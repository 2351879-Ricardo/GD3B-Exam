using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public Transform lookPnt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var hit = new RaycastHit();
        var temp = new Vector3();

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            temp = hit.point;
        }

        lookPnt.position = temp;
    }
}
