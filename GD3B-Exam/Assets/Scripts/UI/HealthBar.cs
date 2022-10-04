using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject _player;
    private PlayerStats _ps;

    public float currentHealth, maxHealth;

    private float _fill;

    private Image _img;
    // Start is called before the first frame update
    void Start()
    {
        _img = GetComponent<Image>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _ps = _player.GetComponent<PlayerStats>();
        maxHealth = _ps.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = _ps._currentHealth;
        
        
        _fill = currentHealth / maxHealth;
        
        
        _img.fillAmount = _fill;
    }
}
