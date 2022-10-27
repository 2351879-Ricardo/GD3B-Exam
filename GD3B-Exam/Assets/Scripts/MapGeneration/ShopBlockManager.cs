using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBlockManager : MonoBehaviour
{
    ShopAccess shopAccess;
    public float EnemyRadius;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckShops()
    {
        Debug.Log("CheckShops");
        Collider[] CheckRadius = Physics.OverlapSphere(Player.position, EnemyRadius);
        for (int i = 0; i < CheckRadius.Length; i++)
        {
            if (CheckRadius[i].gameObject.tag == "Shop")
            {
                shopAccess = CheckRadius[i].gameObject.GetComponent<ShopAccess>();
                shopAccess.CheckShopRadius(EnemyRadius);
            }
        }
    }
}
