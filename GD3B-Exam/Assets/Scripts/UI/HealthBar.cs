using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerStats _playerStats;
    private Slider _healthSlider;
    private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _playerStats = FindObjectOfType<PlayerStats>();
        _maxHealth = _playerStats.maxHealth;
    }

    public void UpdateHealthBar()
    {
        _currentHealth = _playerStats.currentHealth;
        _healthSlider.value = _currentHealth / _maxHealth;
    }
}
