using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    private GameObject _projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeProjectile(GameObject prefab)
    {
        var obj = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        _projectile = obj;
    }

    public void ThrowProjectile()
    {
        var dir = transform.root.forward;
        var rb = _projectile.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = dir * 50;
        _projectile.transform.parent = null;
    }
}
