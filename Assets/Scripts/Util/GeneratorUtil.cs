using System;
using System.Collections.Generic;
using Block;
using PCG;
using UnityEngine;
using Direction = Block.Direction;
using Random = UnityEngine.Random;

namespace Util
{
    public class GeneratorUtil
    {
        private readonly BlockController[,] _roomGrid;
        private readonly GeneratorParams    _generatorParams;
        public GeneratorUtil(BlockController[,] grid, GeneratorParams generatorParams)
        {
            _roomGrid = grid;
            _generatorParams = generatorParams;
        }
        public void ClearRooms(int x, int z, int xSize, int zSize)
        {
            // Clearing for room 0,0
            _roomGrid[z,x].ClearWall(Direction.Up);
            _roomGrid[z,x].ClearWall(Direction.Right);
            if (xSize == 1)
            {
                // Clearing for room 0,zsize
                _roomGrid[z + zSize - 1, x].ClearWall(Direction.Down);
                _roomGrid[z + zSize - 1, x].ClearWall(Direction.Right);
                for (var i = 1; i < zSize - 1; i++)
                {
                    _roomGrid[z + i, x].ClearWall(Direction.Up);
                    _roomGrid[z + i, x].ClearWall(Direction.Down);
                }
            }
            else if (zSize == 1)
            {
                // Clearing for room xsize,0
                _roomGrid[z, x + xSize - 1].ClearWall(Direction.Left);
                _roomGrid[z, x + xSize - 1].ClearWall(Direction.Up);
                for (var i = 1; i < xSize - 1; i++)
                {
                    _roomGrid[z, x + i].ClearWall(Direction.Left);
                    _roomGrid[z, x + i].ClearWall(Direction.Right);
                }
            }
            else
            {
                // Clearing for room 0,zsize
                _roomGrid[z + zSize - 1, x].ClearWall(Direction.Down);
                _roomGrid[z + zSize - 1, x].ClearWall(Direction.Right);
                // Clearing for room xsize,0
                _roomGrid[z, x + xSize - 1].ClearWall(Direction.Left);
                _roomGrid[z, x + xSize - 1].ClearWall(Direction.Up);
                // Clearing for room xsize,zSize
                _roomGrid[z + zSize - 1, x + xSize - 1].ClearWall(Direction.Left);
                _roomGrid[z + zSize - 1, x + xSize - 1].ClearWall(Direction.Down);
                
                // Clearing for the left column
                for (var i = 1; i < xSize - 1; i++)
                {
                    _roomGrid[z, x + i].ClearWall(Direction.Up);
                    _roomGrid[z, x + i].ClearWall(Direction.Right);
                    _roomGrid[z, x + i].ClearWall(Direction.Left);
                }

                // Clearing for the bottom row
                for (var i = 1; i < zSize - 1; i++)
                {
                    _roomGrid[z + i, x].ClearWall(Direction.Right);
                    _roomGrid[z + i, x].ClearWall(Direction.Up);
                    _roomGrid[z + i, x].ClearWall(Direction.Down);
                }
                
                // Clearing for the right column
                for (var i = 1; i < xSize - 1; i++)
                {
                    _roomGrid[z + zSize - 1, x + i].ClearWall(Direction.Right);
                    _roomGrid[z + zSize - 1, x + i].ClearWall(Direction.Left);
                    _roomGrid[z + zSize - 1, x + i].ClearWall(Direction.Down);
                }

                // Clearing for the top row
                for (var i = 1; i < zSize - 1; i++)
                {
                    _roomGrid[z + i, x + xSize - 1].ClearWall(Direction.Up);
                    _roomGrid[z + i, x + xSize - 1].ClearWall(Direction.Down);
                    _roomGrid[z + i, x + xSize - 1].ClearWall(Direction.Left);
                }
                
                // Other clearing
                for (var i = 1; i < zSize - 1; i++)
                {
                    for (var j = 1; j < xSize - 1; j++)
                    {
                        _roomGrid[z + i, x + j].ClearWall(Direction.Left);
                        _roomGrid[z + i, x + j].ClearWall(Direction.Right);
                        _roomGrid[z + i, x + j].ClearWall(Direction.Up);
                        _roomGrid[z + i, x + j].ClearWall(Direction.Down);
                    }
                }
            }
        }
        public void SetBlockType(int x, int z, int xSize, int zSize, BlockType blockType)
        {
            for (var i = z; i < z + zSize; i++)
            {
                for (var j= x; j < x + xSize; j++)
                {
                    _roomGrid[i, j].SetBlockType(blockType);
                    DebugUtil.Instance.DrawDebugText(new Vector3(
                                                        j * _generatorParams.scale + _generatorParams.scale / 2.0f,
                                                        5,
                                                        i * _generatorParams.scale + _generatorParams.scale / 2.0f
                                                        ),
                                        "Room",
                                        Color.red,
                                        12);
                }
            }
        }
        public void CreateDoor(int x, int z, Direction direction)
        {
            _roomGrid[z, x].EnableDoor(direction);
            switch (direction)
            {
                case Direction.Left : 
                    if (x - 1 >= 0)
                        _roomGrid[z, x - 1].ClearWall(Direction.Right);
                    break;
                case Direction.Right :
                    if (x + 1 <= _generatorParams.size)
                        _roomGrid[z, x + 1].ClearWall(Direction.Left);
                    break;
                case Direction.Down : 
                    if (z - 1 >= 0)
                        _roomGrid[z - 1, x].ClearWall(Direction.Up);
                    break;
                case Direction.Up : 
                    if (z + 1 <= _generatorParams.size)
                        _roomGrid[z + 1, x].ClearWall(Direction.Down);
                    break;
                default:
                    break;
            }
        }
        public bool CheckRoomPosition(int x, int z, int xSize, int zSize)
        {
            for (var i = z; i < z + zSize; i++)
            {
                for (var j= x; j < x + xSize; j++)
                {
                    if (!_roomGrid[i, j].IsCorridor())
                        return false;
                }
            }
            return true;
        }
        public BlockController GetNeighbour(BlockController bc, Direction dir)
        {
            var (x, z) = BlockTransformToGrid(bc);
            return dir switch
            {
                Direction.Up => z + 1 < _generatorParams.size  ? _roomGrid[z + 1, x] : null,
                Direction.Right => x + 1 < _generatorParams.size  ? _roomGrid[z, x + 1] : null,
                Direction.Left => x - 1 > 0 ? _roomGrid[z, x - 1] : null,
                Direction.Down => z - 1 > 0 ? _roomGrid[z - 1, x] : null,
                _ => null
            };
        }
        private (int, int) BlockTransformToGrid(BlockController bc)
        {
            var x = (int)bc.transform.position.x;
            var z = (int)bc.transform.position.z;
            return (x / _generatorParams.scale, z / _generatorParams.scale);
        }
        public List<Direction> GetRandomDirectionList()
        {
            var directions = new List<Direction>() { Direction.Up, Direction.Right, Direction.Down, Direction.Left};
            var rndDirections = new List<Direction>();
            while (directions.Count > 0)
            {
                var rnd = Random.Range(0, directions.Count);
                rndDirections.Add(directions[rnd]);
                directions.RemoveAt(rnd);
            }
            return rndDirections;
        }
        public void DeleteWalls(BlockController b1, BlockController b2)
        {
            var bc1 = BlockTransformToGrid(b1);
            var bc2 = BlockTransformToGrid(b2);
            // Item1 = x, Item2 = z
            if (bc1.Item1 > bc2.Item1)
            {
                b1.ClearWall(Direction.Left);
                b2.ClearWall(Direction.Right);
            }else if (bc1.Item1 < bc2.Item1)
            {
                b1.ClearWall(Direction.Right);
                b2.ClearWall(Direction.Left);
            }else if (bc1.Item2 > bc2.Item2)
            {
                b1.ClearWall(Direction.Down);
                b2.ClearWall(Direction.Up);
            }else if (bc1.Item2 < bc2.Item2)
            {
                b1.ClearWall(Direction.Up);
                b2.ClearWall(Direction.Down);
            }
        }
    }
}