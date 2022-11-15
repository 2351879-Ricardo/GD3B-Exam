using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] private float damageTick;
    [SerializeField] private float damage;
    [SerializeField] private float damageRadius;

    public float currentHealth;
    public float time;

    private GameObject _cnvs;
    private CharacterState _cs;
    private HealthBar _healthBar;
    private float _mit;
    
    // Start is called before the first frame update
    void Start()
    {
        _cnvs = GameObject.FindGameObjectWithTag("Canvas");
        _cs = GetComponent<CharacterState>();
        _mit = GetComponentInChildren<Shield>().mitigation;
        _healthBar = FindObjectOfType<HealthBar>();
        
        currentHealth = maxHealth;
        _healthBar.UpdateHealthBar();

        if (FindObjectOfType<BossInit>() != null)
        {
            currentHealth = SceneVariableTransfer.PlayerHealthTransfer;
            _healthBar.UpdateHealthBar();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            EndGame();
        }
    }

    public void TakeDamage(float dmg)
    {
        if (_cs.currentState == CharacterState.PlayerStates.IsBlocking)
        {
            dmg -= _mit*dmg;
        }
        currentHealth -= dmg;
        time = damageTick;
        _healthBar.UpdateHealthBar(); 
    }

    public void KnockBack(Transform origin, float strength)
    {
        var dir = origin.position - transform.position - 0.5f*Vector3.up;
        dir = dir.normalized;
        dir *= -1;

        dir = Vector3.back + 0.5f*Vector3.up;
        
        
    }

    private void EndGame()
    {
        Cursor.visible = true;
        _cnvs.GetComponent<UIManager>().GameOver();
        GetComponent<PlayerInput>().enabled = false;
        
        // Test Spawn Options
        var spawnEnemies = FindObjectsOfType<SpawnEnemies>();
        foreach (var spawner in spawnEnemies)
        {
            spawner.StopSpawn();
        }
        
        Time.timeScale = 0f;
    }

    public void AddHealthToPlayer(int heals)
    {
        currentHealth += heals;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        _healthBar.UpdateHealthBar();
    }
}
