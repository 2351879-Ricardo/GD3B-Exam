using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private float _dmg;

    private void Start()
    {
        _dmg = GetComponentInParent<EnemyController>().EnemySo.EnemyDamagePerAttack;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Punch Thrown");
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerStats>().TakeDamage(_dmg);
            // SendMessageUpwards("DealDamage", _dmg);
        }
    }
}
