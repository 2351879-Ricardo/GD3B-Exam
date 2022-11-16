using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public Transform Path1;
    public Transform Path2;
    public Transform Path3;
    public Transform Path4;
    //public Transform Path5;
    public Transform Shop;
    public Transform Arena;

    private int BlockNumber;

    // Start is called before the first frame update
    void Start()
    {
        BlockNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int Type, int Rotation, int xPos, int zPos)
    {
        Transform Block;
        switch (Type)
        {
            case 0:
                Block = Path1;
                break;
            case 1:
                Block = Path2;
                break;
            case 2:
                Block = Path3;
                break;
            case 3:
                Block = Path4;
                break;
            case 4:
                Block = Shop;
                break;
            default:
                Block = Arena;
                break;
        }
        BlockNumber++;
        if (BlockNumber > 1000)
        {
            BlockNumber = 0;
        }
        var NewBlock = Instantiate(Block, new Vector3(xPos, -0.5f, zPos), Quaternion.Euler(0, Rotation, 0));
        NewBlock.name = BlockNumber.ToString();
    }
}
