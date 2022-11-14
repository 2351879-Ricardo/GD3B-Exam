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
        var x = transform.root.GetComponent<EnemyController>().EnemySo.RangeDamageMult *
                transform.root.GetComponent<EnemyController>().EnemySo.EnemyDamagePerAttack;
        obj.GetComponent<Projectile>().SetDamage(x);
        _projectile = obj;
    }

    public void ThrowProjectile()
    {
        var dir = transform.root.forward;
        var rb = _projectile.GetComponent<Rigidbody>();
        var spd = transform.root.GetComponent<EnemyController>().EnemySo.ProjectileSpeed;
        rb.isKinematic = false;
        rb.velocity = dir * spd;
        _projectile.transform.parent = null;
    }
}
