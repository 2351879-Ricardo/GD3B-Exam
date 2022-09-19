using UnityEngine;

public class EnemyController : MonoBehaviour
{
   [SerializeField] private EnemySO enemySo;

   private void Death()
   {
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
            // Instantiate Reward >> REMEMBER DROP VALUE
         }
      }
   }
}
