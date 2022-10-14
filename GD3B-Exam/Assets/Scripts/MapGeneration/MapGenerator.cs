using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform Player;
    public Transform Path1;
    public Transform Path2;
    public Transform Path3;
    public Transform Path4;
    //public Transform Path5;

    GameObject PreviousGround;
    GameObject CurrentGround;
    RaycastHit hit;

    //class or struct?
    public struct Block
    {
        public int GroundType;
        public int Rotation;
        public int xPosition; //potentially remove
        public int zPosition; //potentially remove
        //change to internal calculation for bools?
        public bool UpOpen;
        public bool DownOpen;
        public bool LeftOpen;
        public bool RightOpen;
        public Block (int _Groundtype, int _Rotation, int _xPosition, int _zPosition, bool _UpOpen, bool _DownOpen, bool _LeftOpen, bool _RightOpen)
        {
            GroundType = _Groundtype;
            Rotation = _Rotation;
            xPosition = _xPosition;
            zPosition = _zPosition;
            UpOpen = _UpOpen;
            DownOpen = _DownOpen;
            LeftOpen = _LeftOpen;
            RightOpen = _RightOpen;
        }
    }

    public Block[,] GridBlocks = new Block[11, 11];

    // Start is called before the first frame update
    void Start()
    {
        InitialGeneration();
        if (Physics.Raycast(Player.transform.position, Vector3.down, out hit))
        {
            PreviousGround = hit.collider.gameObject;
            CurrentGround = PreviousGround;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(Player.transform.position, Vector3.down, out hit))
        {
            CurrentGround = hit.collider.gameObject;
        }
        if (CurrentGround != PreviousGround)
        {
            //entered new square, add row/column
            if (CurrentGround.transform.position.x < PreviousGround.transform.position.x)
            {
                //spawn top row
                NewTopRow();
            }
            if (CurrentGround.transform.position.x > PreviousGround.transform.position.x)
            {
                //spawn bottom row
                NewBottomRow();
            }
            if (CurrentGround.transform.position.z < PreviousGround.transform.position.z)
            {
                //spawn left column
                NewLeftColumn();
            }
            if (CurrentGround.transform.position.z > PreviousGround.transform.position.z)
            {
                //spawn right column
                NewRightColumn();
            }
        }
        PreviousGround = CurrentGround;
    }

    void InitialGeneration()
    {
        //Adding static starting room to center of array
        GridBlocks[4, 4] = new Block(2, 0, -10, -10, false, true, false, true);
        GridBlocks[4, 5] = new Block(0, 0, -10, 0, true, true, true, true);
        GridBlocks[4, 6] = new Block(2, 90, -10, 10, false, true, true, false);
        GridBlocks[5, 4] = new Block(1, 0, 0, -10, true, true, false, true);
        GridBlocks[5, 5] = new Block(0, 0, 0, 0, true, true, true, true);
        GridBlocks[5, 6] = new Block(1, 180, 0, 10, true, true, true, false);
        GridBlocks[6, 4] = new Block(2, -90, 10, -10, true, false, false, true);
        GridBlocks[6, 5] = new Block(1, -90, 10, 0, true, false, true, true);
        GridBlocks[6, 6] = new Block(2, 180, 10, 10, true, false, true, false);
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
                PrevBlockOpen = GridBlocks[row + 1, col].UpOpen;
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
                    AdjBlockOpen = GridBlocks[row, col - 1].RightOpen;
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
                GridBlocks[row, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, NewTopOpen, PrevBlockOpen, AdjBlockOpen, NewRightOpen);
                SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
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
                PrevBlockOpen = GridBlocks[row, col + 1].LeftOpen;
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
                    AdjBlockOpen = GridBlocks[row - 1, col].DownOpen;
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
                GridBlocks[row, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, NewLeftOpen, PrevBlockOpen);
                SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
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
                PrevBlockOpen = GridBlocks[row, col - 1].RightOpen;
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
                    AdjBlockOpen = GridBlocks[row - 1, col].DownOpen;
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
                GridBlocks[row, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, PrevBlockOpen, NewRightOpen);
                SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
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
                PrevBlockOpen = GridBlocks[row - 1, col].DownOpen;
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
                    AdjBlockOpen = GridBlocks[row, col - 1].RightOpen;
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
                GridBlocks[row, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, PrevBlockOpen, NewBottomOpen, AdjBlockOpen, NewRightOpen);
                SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            }
        }
    }

    void NewTopRow()
    {
        //shift array rows down
        for (int col = 0; col < 11; col++)
        {
            for (int row = 10; row > 0; row--)
            {
                GridBlocks[row, col] = GridBlocks[row - 1, col];
            }
        }
        //add row to array using method above
        //block positions must be calculated
        bool PrevBlockOpen;
        bool AdjBlockOpen;
        int xPosition = GridBlocks[0, 0].xPosition - 10;
        int zPosition = GridBlocks[10, 0].zPosition - 10;
        for (int col = 0; col < 11; col++) //column 0 to 10
        {
            zPosition += 10;
            //check below
            PrevBlockOpen = GridBlocks[1, col].UpOpen;
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
                AdjBlockOpen = GridBlocks[0, col - 1].RightOpen;
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
            GridBlocks[0, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, NewTopOpen, PrevBlockOpen, AdjBlockOpen, NewRightOpen);
            SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
        }
    }

    void NewBottomRow()
    {
        //shift array rows up
        for (int col = 0; col < 11; col++)
        {
            for (int row = 0; row < 10; row++)
            {
                GridBlocks[row, col] = GridBlocks[row + 1, col];
            }
        }
        //add row to array using method above
        //block positions must be calculated
        bool PrevBlockOpen;
        bool AdjBlockOpen;
        int xPosition = GridBlocks[10, 0].xPosition + 10;
        int zPosition = GridBlocks[10, 0].zPosition - 10;
        for (int col = 0; col < 11; col++) //column 0 to 10
        {
            zPosition += 10;
            //check above
            PrevBlockOpen = GridBlocks[9, col].DownOpen;
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
                AdjBlockOpen = GridBlocks[10, col - 1].RightOpen;
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
            GridBlocks[10, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, PrevBlockOpen, NewBottomOpen, AdjBlockOpen, NewRightOpen);
            SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
        }
    }

    void NewLeftColumn()
    {
        //shift array column right
        for (int row = 0; row < 11; row++)
        {
            for (int col = 10; col > 0; col--)
            {
                GridBlocks[row, col] = GridBlocks[row, col - 1];
            }
        }
        //add column to array using method above
        //block positions must be calculated
        bool PrevBlockOpen;
        bool AdjBlockOpen;
        int xPosition = GridBlocks[0, 0].xPosition - 10;
        int zPosition = GridBlocks[0, 0].zPosition - 10;
        for (int row = 0; row < 11; row++) //row 0 to 10
        {
            xPosition += 10;
            //check right
            PrevBlockOpen = GridBlocks[row, 1].LeftOpen;
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
                AdjBlockOpen = GridBlocks[row - 1, 0].DownOpen;
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
            GridBlocks[row, 0] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, NewLeftOpen, PrevBlockOpen);
            SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
        }
    }

    void NewRightColumn()
    {
        //shift array column left
        for (int row = 0; row < 11; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                GridBlocks[row, col] = GridBlocks[row, col + 1];
            }
        }
        //add column to array using method above
        //block positions must be calculated
        bool PrevBlockOpen;
        bool AdjBlockOpen;
        int xPosition = GridBlocks[0, 0].xPosition - 10;
        int zPosition = GridBlocks[0, 10].zPosition + 10;
        for (int row = 0; row < 11; row++) //row 0 to 10
        {
            xPosition += 10;
            //check left
            PrevBlockOpen = GridBlocks[row, 9].RightOpen;
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
                AdjBlockOpen = GridBlocks[row - 1, 10].DownOpen;
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
            GridBlocks[row, 10] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, PrevBlockOpen, NewRightOpen);
            SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
        }
    }

    void SpawnBlock(int Type, int Rotation, int xPos, int zPos)
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
            default:
                Block = Path4;
                break;
        }
        Instantiate(Block, new Vector3(xPos, -0.5f, zPos), Quaternion.Euler(0, Rotation, 0));
    }
}

/*
    Ground types:
    0 - no walls
    1 - 1 wall
    2 - 2 walls connected
    3 - 2 walls opposite
    4 - 3 walls (scrapped)

    Type, rotation and locations(of walls):
    0   0
    1   0       left
    1   90      top
    1   180     right
    1   -90     bottom
    2   0       top, left
    2   90      top, right
    2   180     bottom, right
    2   -90     bottom, left
    3   0       left, right
    3   90      top, bottom

    Blocks for connecting:
    Both open:
    0, 1, 2
    One open one closed:
    1, 2, 3
    Both closed:
    2
*/