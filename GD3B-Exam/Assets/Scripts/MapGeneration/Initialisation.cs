using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialisation : MonoBehaviour
{
    public Transform Player;
    public Transform Path1;
    public Transform Path2;
    public Transform Path3;
    public Transform Path4;
    //public Transform Path5;

    public MapGenerator mapGenerator;
    public SpawnBlock spawnBlock;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialise()
    {
        //Adding static starting room to center of array
        mapGenerator.GridBlocks[4, 4] = new MapGenerator.Block(2, 0, -10, -10, false, true, false, true);
        mapGenerator.GridBlocks[4, 5] = new MapGenerator.Block(0, 0, -10, 0, true, true, true, true);
        mapGenerator.GridBlocks[4, 6] = new MapGenerator.Block(2, 90, -10, 10, false, true, true, false);
        mapGenerator.GridBlocks[5, 4] = new MapGenerator.Block(1, 0, 0, -10, true, true, false, true);
        mapGenerator.GridBlocks[5, 5] = new MapGenerator.Block(0, 0, 0, 0, true, true, true, true);
        mapGenerator.GridBlocks[5, 6] = new MapGenerator.Block(1, 180, 0, 10, true, true, true, false);
        mapGenerator.GridBlocks[6, 4] = new MapGenerator.Block(2, -90, 10, -10, true, false, false, true);
        mapGenerator.GridBlocks[6, 5] = new MapGenerator.Block(1, -90, 10, 0, true, false, true, true);
        mapGenerator.GridBlocks[6, 6] = new MapGenerator.Block(2, 180, 10, 10, true, false, true, false);
        //starts from front moving outwards, then left and right moving outwards (not below bottom of center box), then back moving outwards
        //same formula as for adding rows and columns, different dimensions
        bool PrevBlockOpen;
        bool AdjBlockOpen;
        int xPosition;
        int zPosition;
        //front
        for (int row = 3; row > -1; row--) //row 3 to 0
        {
            xPosition = ((row - 5) * 10);
            for (int col = 4; col < 7; col++) //column 4 to 6
            {
                zPosition = ((col - 5) * 10);
                //check below
                PrevBlockOpen = mapGenerator.GridBlocks[row + 1, col].UpOpen;
                //check adjacent (left)
                if (col == 4)
                {
                    //decide if side is wall or not randomly
                    if (Random.Range(0, 2) == 0)
                    {
                        AdjBlockOpen = true;
                    }
                    else
                    {
                        AdjBlockOpen = false;
                    }
                }
                else
                {
                    AdjBlockOpen = mapGenerator.GridBlocks[row, col - 1].RightOpen;
                }
                //pick random possible block, get rotation, get state of unconnected sides
                int BlockTypeChosen = 0;
                int BlockRotation = 0;
                bool NewTopOpen = true;
                bool NewRightOpen = true;
                if (PrevBlockOpen && AdjBlockOpen) //both open
                {
                    BlockTypeChosen = Random.Range(0, 3);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, two possible directions
                        if (Random.Range(0, 2) == 0)
                        {
                            BlockRotation = 90;
                            NewTopOpen = false;
                            NewRightOpen = true;
                        }
                        else
                        {
                            BlockRotation = 180;
                            NewTopOpen = true;
                            NewRightOpen = false;
                        }
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 90;
                        NewTopOpen = false;
                        NewRightOpen = false;
                    }
                }
                else if (PrevBlockOpen && !AdjBlockOpen) //left closed, bottom open         //all 0 rotation
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = 0;
                        NewTopOpen = true;
                        NewRightOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 0;
                        NewTopOpen = false;
                        NewRightOpen = true;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 0;
                        NewTopOpen = true;
                        NewRightOpen = false;
                    }
                }
                else if (!PrevBlockOpen && AdjBlockOpen) //left open, bottom closed
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = -90;
                        NewTopOpen = true;
                        NewRightOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 180;
                        NewTopOpen = true;
                        NewRightOpen = false;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 90;
                        NewTopOpen = false;
                        NewRightOpen = true;
                    }
                }
                else if (!PrevBlockOpen && !AdjBlockOpen) //both closed
                {
                    BlockTypeChosen = 2;
                    BlockRotation = -90;
                    NewTopOpen = true;
                    NewRightOpen = true;
                }
                //add new block to array and spawn on map
                mapGenerator.GridBlocks[row, col] = new MapGenerator.Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, NewTopOpen, PrevBlockOpen, AdjBlockOpen, NewRightOpen);
                //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
                spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            }
        }
        //left
        for (int col = 3; col > -1; col--) //column 3 to 0
        {
            zPosition = ((col - 5) * 10);
            for (int row = 0; row < 7; row++) //row 0 to 6
            {
                xPosition = ((row - 5) * 10);
                //check right
                PrevBlockOpen = mapGenerator.GridBlocks[row, col + 1].LeftOpen;
                //check adjacent (top)
                if (row == 0)
                {
                    //decide if side is wall or not randomly
                    if (Random.Range(0, 2) == 0)
                    {
                        AdjBlockOpen = true;
                    }
                    else
                    {
                        AdjBlockOpen = false;
                    }
                }
                else
                {
                    AdjBlockOpen = mapGenerator.GridBlocks[row - 1, col].DownOpen;
                }
                //pick random possible block, get rotation, get state of unconnected sides
                int BlockTypeChosen = 0;
                int BlockRotation = 0;
                bool NewBottomOpen = true;
                bool NewLeftOpen = true;
                if (PrevBlockOpen && AdjBlockOpen) //both open
                {
                    BlockTypeChosen = Random.Range(0, 3);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, two possible directions
                        if (Random.Range(0, 2) == 0)
                        {
                            BlockRotation = 0;
                            NewBottomOpen = true;
                            NewLeftOpen = false;
                        }
                        else
                        {
                            BlockRotation = -90;
                            NewBottomOpen = false;
                            NewLeftOpen = true;
                        }
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = -90;
                        NewBottomOpen = false;
                        NewLeftOpen = false;
                    }
                }
                else if (PrevBlockOpen && !AdjBlockOpen) //top closed, right open
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = true;
                        NewLeftOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 0;
                        NewBottomOpen = true;
                        NewLeftOpen = false;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = false;
                        NewLeftOpen = true;
                    }
                }
                else if (!PrevBlockOpen && AdjBlockOpen) //top open, right closed
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = 180;
                        NewBottomOpen = true;
                        NewLeftOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 180;
                        NewBottomOpen = false;
                        NewLeftOpen = true;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 0;
                        NewBottomOpen = true;
                        NewLeftOpen = false;
                    }
                }
                else if (!PrevBlockOpen && !AdjBlockOpen) //both closed
                {
                    BlockTypeChosen = 2;
                    BlockRotation = 90;
                    NewBottomOpen = true;
                    NewLeftOpen = true;
                }
                //add new block to array and spawn on map
                mapGenerator.GridBlocks[row, col] = new MapGenerator.Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, NewLeftOpen, PrevBlockOpen);
                //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
                spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            }
        }
        //right
        for (int col = 7; col < 11; col++) //column 7 to 10
        {
            zPosition = ((col - 5) * 10);
            for (int row = 0; row < 7; row++) //row 0 to 6
            {
                xPosition = ((row - 5) * 10);
                //check left
                PrevBlockOpen = mapGenerator.GridBlocks[row, col - 1].RightOpen;
                //check adjacent (top)
                if (row == 0)
                {
                    //decide if side is wall or not randomly
                    if (Random.Range(0, 2) == 0)
                    {
                        AdjBlockOpen = true;
                    }
                    else
                    {
                        AdjBlockOpen = false;
                    }
                }
                else
                {
                    AdjBlockOpen = mapGenerator.GridBlocks[row - 1, col].DownOpen;
                }
                //pick random possible block, get rotation, get state of unconnected sides
                int BlockTypeChosen = 0;
                int BlockRotation = 0;
                bool NewBottomOpen = true;
                bool NewRightOpen = true;
                if (PrevBlockOpen && AdjBlockOpen) //both open
                {
                    BlockTypeChosen = Random.Range(0, 3);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, two possible directions
                        if (Random.Range(0, 2) == 0)
                        {
                            BlockRotation = 180;
                            NewBottomOpen = true;
                            NewRightOpen = false;
                        }
                        else
                        {
                            BlockRotation = -90;
                            NewBottomOpen = false;
                            NewRightOpen = true;
                        }
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 180;
                        NewBottomOpen = false;
                        NewRightOpen = false;
                    }
                }
                else if (PrevBlockOpen && !AdjBlockOpen) //top closed, left open
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = true;
                        NewRightOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = true;
                        NewRightOpen = false;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = false;
                        NewRightOpen = true;
                    }
                }
                else if (!PrevBlockOpen && AdjBlockOpen) //top open, left closed
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = 0;
                        NewBottomOpen = true;
                        NewRightOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = -90;
                        NewBottomOpen = false;
                        NewRightOpen = true;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 0;
                        NewBottomOpen = true;
                        NewRightOpen = false;
                    }
                }
                else if (!PrevBlockOpen && !AdjBlockOpen) //both closed
                {
                    BlockTypeChosen = 2;
                    BlockRotation = 0;
                    NewBottomOpen = true;
                    NewRightOpen = true;
                }
                //add new block to array and spawn on map
                mapGenerator.GridBlocks[row, col] = new MapGenerator.Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, PrevBlockOpen, NewRightOpen);
                //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
                spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            }
        }
        //bottom
        for (int row = 7; row < 11; row++) //row 7 to 10
        {
            xPosition = ((row - 5) * 10);
            for (int col = 0; col < 11; col++) //column 0 to 10
            {
                zPosition = ((col - 5) * 10);
                //check above
                PrevBlockOpen = mapGenerator.GridBlocks[row - 1, col].DownOpen;
                //check adjacent (left)
                if (col == 0)
                {
                    //decide if side is wall or not randomly
                    if (Random.Range(0, 2) == 0)
                    {
                        AdjBlockOpen = true;
                    }
                    else
                    {
                        AdjBlockOpen = false;
                    }
                }
                else
                {
                    AdjBlockOpen = mapGenerator.GridBlocks[row, col - 1].RightOpen;
                }
                //pick random possible block, get rotation, get state of unconnected sides
                int BlockTypeChosen = 0;
                int BlockRotation = 0;
                bool NewBottomOpen = true;
                bool NewRightOpen = true;
                if (PrevBlockOpen && AdjBlockOpen) //both open
                {
                    BlockTypeChosen = Random.Range(0, 3);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, two possible directions
                        if (Random.Range(0, 2) == 0)
                        {
                            BlockRotation = -90;
                            NewBottomOpen = false;
                            NewRightOpen = true;
                        }
                        else
                        {
                            BlockRotation = 180;
                            NewBottomOpen = true;
                            NewRightOpen = false;
                        }
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 180;
                        NewBottomOpen = false;
                        NewRightOpen = false;
                    }
                }
                else if (PrevBlockOpen && !AdjBlockOpen) //top open, left closed
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = 0;
                        NewBottomOpen = true;
                        NewRightOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = -90;
                        NewBottomOpen = false;
                        NewRightOpen = true;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 0;
                        NewBottomOpen = true;
                        NewRightOpen = false;
                    }
                }
                else if (!PrevBlockOpen && AdjBlockOpen) //top closed, left open
                {
                    BlockTypeChosen = Random.Range(1, 4);
                    //choose / fix rotation
                    if (BlockTypeChosen == 1)
                    {
                        //one wall, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = true;
                        NewRightOpen = true;
                    }
                    else if (BlockTypeChosen == 2)
                    {
                        //two adjacent walls, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = true;
                        NewRightOpen = false;
                    }
                    else
                    {
                        //two opposite walls, one possible direction
                        BlockRotation = 90;
                        NewBottomOpen = false;
                        NewRightOpen = true;
                    }
                }
                else if (!PrevBlockOpen && !AdjBlockOpen) //both closed
                {
                    BlockTypeChosen = 2;
                    BlockRotation = 0;
                    NewBottomOpen = true;
                    NewRightOpen = true;
                }
                //add new block to array and spawn on map
                mapGenerator.GridBlocks[row, col] = new MapGenerator.Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, PrevBlockOpen, NewBottomOpen, AdjBlockOpen, NewRightOpen);
                //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
                spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            }
        }
    }
}
