using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Offhand
{
    public float mitigation;
    public string animationParamName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string ActivateAbility()
    {
        gameObject.SendMessageUpwards("Blocking");
        return animationParamName;
    }
}
