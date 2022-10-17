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
        LockShop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(Player.transform.position, Vector3.down, out hit))
        {
            CurrentGround = hit.collider.gameObject;
        }
        if (CurrentGround.tag == "Shop")
        {
            //allow access to shop
        }
    }

    public void LockShop()
    {
        WallLock.gameObject.SetActive(true);
    }

    public void UnlockShop()
    {
        WallLock.gameObject.SetActive(false);
    }
}
