using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAccess : MonoBehaviour
{
    public GameObject Player;
    public Transform WallLock;
    GameObject CurrentGround;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("TestPlayer");
        UnlockShop();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void CheckShopRadius(float EnemyRadius)
    {
        Debug.Log("CheckEnemies");
        bool EnemiesFound = false;
        Collider[] CheckRadius = Physics.OverlapSphere(transform.position, EnemyRadius);
        for (int i = 0; i < CheckRadius.Length; i++)
        {
            if (CheckRadius[i].gameObject.tag == "Enemy")
            {
                EnemiesFound = true;
            }
        }
        if (EnemiesFound)
        {
            LockShop();
        }
        else
        {
            UnlockShop();
        }
    }

    void LockShop()
    {
        if (Physics.Raycast(Player.transform.position, Vector3.down, out hit))
        {
            CurrentGround = hit.collider.gameObject;
        }
        else
        {
            CurrentGround = null;
        }
        if (CurrentGround == this.gameObject)
        {
            WallLock.gameObject.SetActive(false);
        }
        else
        {
            WallLock.gameObject.SetActive(true);
        }
    }

    void UnlockShop()
    {
        WallLock.gameObject.SetActive(false);
    }
}
