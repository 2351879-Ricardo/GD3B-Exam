using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _dmg;

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
    }
}
