using UnityEngine;

public class ShopBlockManager : MonoBehaviour
{
    public float enemyRadius;
    
    private Transform _player;
    private ShopAccess _shopAccess;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerStats>().transform;
    }

    public void CheckShops()
    {
        Debug.Log("CheckShops");
        var checkRadius = Physics.OverlapSphere(_player.position, enemyRadius);
        foreach (var collider in checkRadius)
        {
            if (!collider.gameObject.CompareTag("Shop")) continue;
            _shopAccess = collider.gameObject.GetComponent<ShopAccess>();
            _shopAccess.CheckShopRadius(enemyRadius);
        }
    }
}
