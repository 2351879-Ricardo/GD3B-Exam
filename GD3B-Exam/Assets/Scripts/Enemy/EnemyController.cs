using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
   [SerializeField] private GameObject damageTextUI;
   [SerializeField] private EnemySO enemySo;
   [SerializeField] private float health;

   [SerializeField] private GameObject projectile;

   ShopBlockManager shopBlockManager;

   private void Start()
   {
      InitEnemy();
      GetComponent<NavMeshAgent>().speed = EnemySo.EnemySpeed;
      
      shopBlockManager = FindObjectOfType<ShopBlockManager>();
      shopBlockManager.CheckShops();
   }

   public void TakeDamage(float damage)
   {
      var height = GetComponentInChildren<CapsuleCollider>().height;
      var damagePopup = Instantiate(damageTextUI, (transform.position + new Vector3(0f, height/2f, 0f)), Quaternion.identity).GetComponent<DamagePopup>();
      damagePopup.SetDamageText((int) damage);
      health -= damage;
      
      GetComponent<EnemyStateManager>().Flinch();

      if (GetComponentInChildren<BossBar>() != null)
      {
         var bossBar = GetComponentInChildren<BossBar>();
         bossBar.UpdateHealthBar();
      }

      if (health <= 0)
      {
         Death();
      }
   }
   
   private void Death()
   {
      Debug.Log("Dead");
      shopBlockManager.CheckShops();
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

   public void SpawnProjectile()
   {
      BroadcastMessage("MakeProjectile", projectile);
   }

   public void LaunchProjectile()
   {
      BroadcastMessage("ThrowProjectile");   
   }

   private void InitEnemy()
   {
      Debug.Log("Init Enemy");
      health = enemySo.EnemyHealth;
      Debug.Log("Health: "+ health);
      
      if (GetComponentInChildren<BossBar>() != null)
      {
         var bossBar = GetComponentInChildren<BossBar>();
         bossBar.UpdateHealthBar();
      }
      
   }

   public void ChargeEnd()
   {
      BroadcastMessage("EndCharge");
   }

   public EnemySO EnemySo => enemySo;
   public GameObject Projectile => projectile;

   public float Health => health;
}
