using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
   [SerializeField] private GameObject damageTextUI;
   [SerializeField] private EnemySO enemySo;
   [SerializeField] private float health;

   private void Start()
   {
      InitEnemy();
      GetComponent<NavMeshAgent>().speed = EnemySo.EnemySpeed/2f;
   }

   public void TakeDamage(float damage)
   {
      var height = GetComponentInChildren<CapsuleCollider>().height;
      var damagePopup = Instantiate(damageTextUI, (transform.position + new Vector3(0f, height/2f, 0f)), Quaternion.identity).GetComponent<DamagePopup>();
      damagePopup.SetDamageText((int) damage);
      health -= damage;

      if (health <= 0)
      {
         Death();
      }
   }
   
   private void Death()
   {
      Debug.Log("Dead");
      GenerateLoot();
      // Send AI Information >> AI 
      Destroy(gameObject);
   }

   private void GenerateLoot()
   {
      foreach (var lootDrop in enemySo.EnemyDropTable)
      {
         var roll = Random.Range(0f, 1f);
         if (roll <= lootDrop.LootDropChance)
         {
            var resourceDrop = Instantiate(lootDrop.ResourcePrefab, transform.position, Quaternion.identity);
            resourceDrop.GetComponent<ResourceManager>().ResourceAmount = lootDrop.LootDropAmount;
         }
      }
   }

   private void InitEnemy()
   {
      health = enemySo.EnemyHealth;
   }

   public EnemySO EnemySo => enemySo;
}
