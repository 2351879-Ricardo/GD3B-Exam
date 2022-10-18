using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAccess : MonoBehaviour
{
    public GameObject Player;
    public Transform WallLock;
    GameObject CurrentGround;
    RaycastHit hit;
    bool IsLocked = true;
    public bool InShop;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("TestPlayer");
        InShop = false;
        LockShop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Player.transform.position, Vector3.down, out hit))
        {
            CurrentGround = hit.collider.gameObject;
        }
        if (CurrentGround.tag == "Shop")
        {
            //access and check bool in other script. when true, allow access to shop. when false, don't allow access
            InShop = true;
        }
        else
        {
            InShop = false;
        }
        //replace condition for shop to be unlocked, call function UnlockShop()
        //if (no enemies nearby) {UnlockShop();}
        //else if (enemies nearby) {LockShop();}
        {//this section will be removed once proper conditions have been coded
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsLocked)
                {
                    UnlockShop();
                }
                else
                {
                    LockShop();
                }
            }
        }
    }

    public void LockShop()
    {
        IsLocked = true;
        WallLock.gameObject.SetActive(true);
    }

    public void UnlockShop()
    {
        IsLocked = false;
        WallLock.gameObject.SetActive(false);
    }
}
