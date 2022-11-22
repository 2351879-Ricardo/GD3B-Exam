using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _dmg;
    [SerializeField] private float lifeTime;
    private float _timer;
    private bool _hit = false;

    private void Start()
    {
        _timer = lifeTime;
    }

    private void Update()
    {
        if (_hit)
        {
            _timer -= Time.deltaTime;
        }

        if (_hit && _timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        _dmg = damage;
        Debug.Log(_dmg);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(_dmg);
        }
        _hit = true;

    }
}
