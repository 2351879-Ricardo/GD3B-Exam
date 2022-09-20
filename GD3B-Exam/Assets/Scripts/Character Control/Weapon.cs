using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum DamageType
    {
        RANGE,
        BLUNT,
        MAGIC
    };

    [SerializeField] private DamageType damageType;
    
    public float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
