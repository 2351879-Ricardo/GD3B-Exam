using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaDoors : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) LoadBoss();
    }

    private void LoadBoss()
    {
        // Save PLayer Details
        SceneVariableTransfer.PlayerHealthTransfer = FindObjectOfType<PlayerStats>().currentHealth;
        var inv = FindObjectOfType<InventoryManager>();
        SceneVariableTransfer.Inv1Amount = inv.InventoryList[0].resourceCount;
        SceneVariableTransfer.Inv2Amount = inv.InventoryList[1].resourceCount;
        
        // Load Boss Level
        SceneManager.LoadScene(2);
    }
}
