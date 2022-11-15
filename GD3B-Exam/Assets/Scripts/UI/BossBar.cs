using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Slider healthSlider;
    
    private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _maxHealth = enemyController.EnemySo.EnemyHealth;
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        _currentHealth = enemyController.Health;
        healthSlider.value = _currentHealth / _maxHealth;
    }
}
