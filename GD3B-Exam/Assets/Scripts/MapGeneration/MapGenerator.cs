using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapGenerator : MonoBehaviour
{
    public Transform Player;
    public Transform Path1;
    public Transform Path2;
    public Transform Path3;
    public Transform Path4;
    public Transform Shop;
    public Transform Arena;

    Tuple<int,int> PreviousGround;
    Tuple<int,int> CurrentGround;
    enum Direction
    {
        None,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        Left,
        UpLeft
    }

    public float ShopSpawnRate;
    public float ArenaSpawnRate;
    public float Path1Rate;
    public float Path2Rate;
    public float Path3Rate;
    public float Path4Rate;
    int ShopPercent;
    int ArenaPercent;
    int Path1Percent;
    int Path2Percent;
    int Path3Percent;
    int Path4Percent;
    int TotalPercent;
    public float Timer;

    //class or struct?
    public struct Block
    {
        public int GroundType;
        public int Rotation;
        public int xPosition;
        public int zPosition;
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
    //private Queue<Direction> GenerateOrder = new Queue<Direction>();
    private Direction AddDirection;

    public Initialisation initialisation;
    public SpawnBlock spawnBlock;

    private Tuple<int,int> PosToTuple(Vector3 input)
    {
        int x = Mathf.FloorToInt((input.x + 5) / 10);
        int z = Mathf.FloorToInt((input.z + 5) / 10);
        return new Tuple<int, int>(x, z);
    }

    private Direction CheckPosition(Vector3 input)
    {
        PreviousGround = PosToTuple(input);
        if (CurrentGround == null)
        {
            CurrentGround = PreviousGround;
            return Direction.None;
        }
        if (PreviousGround.Item1 != CurrentGround.Item1 || PreviousGround.Item2 != CurrentGround.Item2)
        {
            int xDiff = PreviousGround.Item1 - CurrentGround.Item1;
            int zDiff = PreviousGround.Item2 - CurrentGround.Item2;
            CurrentGround = PreviousGround;
            if (xDiff == -1)
            {
                switch (zDiff)
                {
                    case -1:
                        return Direction.UpLeft;
                    case 0:
                        return Direction.Up;
                    case 1:
                        return Direction.UpRight;
                    default:
                        throw new Exception("Invalid direction");
                }
            }
            else if (xDiff == 0)
            {
                switch (zDiff)
                {
                    case -1:
                        return Direction.Left;
                    case 1:
                        return Direction.Right;
                    default:
                        throw new Exception("Invalid direction");
                }
            }
            else if (xDiff == 1)
            {
                switch (zDiff)
                {
                    case -1:
                        return Direction.DownLeft;
                    case 0:
                        return Direction.Down;
                    case 1:
                        return Direction.DownRight;
                    default:
                        throw new Exception("Invalid direction");
                }
            }
            else
            {
                throw new Exception("Invalid direction");
            }
        }
        return Direction.None;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setting path rates, initialising variables, calling initialisation script for start of map
        ShopPercent = Mathf.RoundToInt(ShopSpawnRate);
        ArenaPercent = Mathf.RoundToInt(ArenaSpawnRate);
        Path1Percent = Mathf.RoundToInt(Path1Rate);
        Path2Percent = Mathf.RoundToInt(Path2Rate);
        Path3Percent = Mathf.RoundToInt(Path3Rate);
        Path4Percent = Mathf.RoundToInt(Path4Rate);
        if (ShopPercent <= 0)
        {
            ShopPercent = 1;
        }
        if (ArenaPercent <= 0)
        {
            ArenaPercent = 1;
        }
        if (Path1Percent <= 0)
        {
            Path1Percent = 1;
        }
        if (Path2Percent <= 0)
        {
            Path2Percent = 1;
        }
        if (Path3Percent <= 0)
        {
            Path3Percent = 1;
        }
        if (Path4Percent <= 0)
        {
            Path4Percent = 1;
        }
        TotalPercent = ShopPercent + ArenaPercent + Path1Percent + Path2Percent + Path3Percent + Path4Percent;
        Timer = 0f;
        initialisation.Initialise();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Timer += Time.fixedDeltaTime;
        AddDirection = CheckPosition(Player.position);
        switch (AddDirection)
        {
            case Direction.Up:
                NewTopRow();
                break;
            case Direction.Down:
                NewBottomRow();
                break;
            case Direction.Left:
                NewLeftColumn();
                break;
            case Direction.Right:
                NewRightColumn();
                break;
            case Direction.UpLeft:
                NewTopRow();
                NewLeftColumn();
                break;
            case Direction.UpRight:
                NewTopRow();
                NewRightColumn();
                break;
            case Direction.DownLeft:
                NewBottomRow();
                NewLeftColumn();
                break;
            case Direction.DownRight:
                NewBottomRow();
                NewRightColumn();
                break;
            default:
                break;
        }
    }

    void NewTopRow()
    {
        Debug.Log("Making top row");
        //shift array rows down
        for (int col = 0; col < 11; col++)
        {
            for (int row = 10; row > 0; row--)
            {
                GridBlocks[row, col] = GridBlocks[row - 1, col];
            }
        }
        //add row to array using method in initialisation script
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
                if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(0, 3);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path4Percent));
                if (RandomNum < Path1Percent)
                {
                    BlockTypeChosen = 0;
                }
                else if (RandomNum < (Path1Percent + Path2Percent))
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
                //path 1 (0 walls), shop and arena have 0 rotation and both new sides are open
                //choose / fix rotation
                if (BlockTypeChosen == 1)
                {
                    //one wall, two possible directions
                    if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                //BlockTypeChosen = 2;
                int RandomNum = UnityEngine.Random.Range(0, (Path2Percent + ShopPercent + ArenaPercent));
                if (RandomNum < Path3Percent)
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (Path3Percent + ShopPercent + ArenaPercent))
                {
                    BlockTypeChosen = 5;
                }
                if (BlockTypeChosen == 2)
                {
                    BlockRotation = -90;
                    NewTopOpen = true;
                    NewRightOpen = true;
                }
            }
            //add new block to array and spawn on map
            GridBlocks[0, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, NewTopOpen, PrevBlockOpen, AdjBlockOpen, NewRightOpen);
            //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
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
                if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(0, 3);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path4Percent));
                if (RandomNum < Path1Percent)
                {
                    BlockTypeChosen = 0;
                }
                else if (RandomNum < (Path1Percent + Path2Percent))
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
                //choose / fix rotation
                if (BlockTypeChosen == 1)
                {
                    //one wall, two possible directions
                    if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                int RandomNum = UnityEngine.Random.Range(0, (Path2Percent + ShopPercent + ArenaPercent));
                if (RandomNum < Path3Percent)
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (Path3Percent + ShopPercent + ArenaPercent))
                {
                    BlockTypeChosen = 5;
                }
                if (BlockTypeChosen == 2)
                {
                    BlockRotation = 0;
                    NewBottomOpen = true;
                    NewRightOpen = true;
                }
            }
            //add new block to array and spawn on map
            GridBlocks[10, col] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, PrevBlockOpen, NewBottomOpen, AdjBlockOpen, NewRightOpen);
            //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
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
                if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(0, 3);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path4Percent));
                if (RandomNum < Path1Percent)
                {
                    BlockTypeChosen = 0;
                }
                else if (RandomNum < (Path1Percent + Path2Percent))
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
                //choose / fix rotation
                if (BlockTypeChosen == 1)
                {
                    //one wall, two possible directions
                    if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                int RandomNum = UnityEngine.Random.Range(0, (Path2Percent + ShopPercent + ArenaPercent));
                if (RandomNum < Path3Percent)
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (Path3Percent + ShopPercent + ArenaPercent))
                {
                    BlockTypeChosen = 5;
                }
                if (BlockTypeChosen == 2)
                {
                    BlockRotation = 90;
                    NewBottomOpen = true;
                    NewLeftOpen = true;
                }
            }
            //add new block to array and spawn on map
            GridBlocks[row, 0] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, NewLeftOpen, PrevBlockOpen);
            //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
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
                if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(0, 3);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path4Percent));
                if (RandomNum < Path1Percent)
                {
                    BlockTypeChosen = 0;
                }
                else if (RandomNum < (Path1Percent + Path2Percent))
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path1Percent + Path2Percent + Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
                //choose / fix rotation
                if (BlockTypeChosen == 1)
                {
                    //one wall, two possible directions
                    if (UnityEngine.Random.Range(0, 2) == 0)
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                //BlockTypeChosen = Random.Range(1, 4);
                int RandomNum = UnityEngine.Random.Range(0, (TotalPercent - Path1Percent));
                if (RandomNum < Path2Percent)
                {
                    BlockTypeChosen = 1;
                }
                else if (RandomNum < (Path2Percent + Path3Percent))
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent))
                {
                    BlockTypeChosen = 3;
                }
                else if (RandomNum < (Path2Percent + Path3Percent + Path4Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (TotalPercent - Path4Percent))
                {
                    BlockTypeChosen = 5;
                }
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
                int RandomNum = UnityEngine.Random.Range(0, (Path2Percent + ShopPercent + ArenaPercent));
                if (RandomNum < Path3Percent)
                {
                    BlockTypeChosen = 2;
                }
                else if (RandomNum < (Path3Percent + ShopPercent))
                {
                    BlockTypeChosen = 4;
                }
                else if (RandomNum < (Path3Percent + ShopPercent + ArenaPercent))
                {
                    BlockTypeChosen = 5;
                }
                if (BlockTypeChosen == 2)
                {
                    BlockRotation = 0;
                    NewBottomOpen = true;
                    NewRightOpen = true;
                }
            }
            //add new block to array and spawn on map
            GridBlocks[row, 10] = new Block(BlockTypeChosen, BlockRotation, xPosition, zPosition, AdjBlockOpen, NewBottomOpen, PrevBlockOpen, NewRightOpen);
            //SpawnBlock(BlockTypeChosen, BlockRotation, xPosition, zPosition);
            spawnBlock.Spawn(BlockTypeChosen, BlockRotation, xPosition, zPosition);
        }
    }
}

/*
    Ground types:
    0 - no walls
    1 - 1 wall
    2 - 2 walls connected
    3 - 2 walls opposite

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