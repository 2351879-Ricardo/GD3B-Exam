using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum DamageType
    {
        RANGE,
        BLUNT,
        MAGIC
    };

    public DamageType damageType;
    
    public float damage;
}
