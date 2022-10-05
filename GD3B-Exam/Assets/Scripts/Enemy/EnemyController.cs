using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
   [SerializeField] private EnemySO enemySo;
   [SerializeField] private float health;

   private void Start()
   {
      InitEnemy();
   }

   public void TakeDamage(float damage)
   {
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
