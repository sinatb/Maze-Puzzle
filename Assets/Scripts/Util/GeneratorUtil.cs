using System.Collections.Generic;
using Block;
using PCG;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Direction = Block.Direction;
using Random = UnityEngine.Random;

namespace Util
{
    /// <summary>
    /// A Utility class, to be only used in generator.
    /// </summary>
    public class GeneratorUtil
    {
        /// <summary>
        /// The grid of all rooms in the map. The first element is z and the second one x
        /// </summary>
        private readonly BlockController[,] _roomGrid;

        private readonly Pill[,] _pillars;
        /// <summary>
        /// Parameters of the Generator, Passed in Generator Start Method. Is-a Scriptable Object 
        /// </summary>
        private readonly GeneratorParams    _generatorParams;
        public GeneratorUtil(BlockController[,] grid, GeneratorParams generatorParams)
        {
            _roomGrid = grid;
            _generatorParams = generatorParams;
        }
        /// <summary>
        /// Helper function to clear a room
        /// </summary>
        /// <param name="x">Starting x coordinate of the room</param>
        /// <param name="z">Starting z coordinate of the room</param>
        /// <param name="xSize">Size of room in x-axis</param>
        /// <param name="zSize">Size of room in z-axis</param>
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
        /// <summary>
        /// Given a block type sets all Blocks in a rectangular area to that type
        /// </summary>
        /// <param name="x">Starting x coordinate</param>
        /// <param name="z">Starting z coordinate</param>
        /// <param name="xSize">Size in x-axis</param>
        /// <param name="zSize">Size in z-axis</param>
        /// <param name="blockType">Type of blocks in area. Is-a Scriptable object set in room</param>
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
        /// <summary>
        /// Activates door in a block
        /// </summary>
        /// <param name="x">Starting x coordinates</param>
        /// <param name="z">Starting z coordinates</param>
        /// <param name="direction">Direction of the door</param>
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
        /// <summary>
        /// Checks if a room can be placed at requested position. Rooms cant be placed on other rooms
        /// </summary>
        /// <param name="x">Starting x coordinate</param>
        /// <param name="z">Starting z coordinate</param>
        /// <param name="xSize">Size in x-axis</param>
        /// <param name="zSize">Size in z-axis</param>
        /// <returns>If the room can be placed in requested position</returns>
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
        /// <summary>
        /// Gets neighbour of current block in one of four directions
        /// </summary>
        /// <param name="bc">Block controller of current block</param>
        /// <param name="dir">Direction of requested neighbour</param>
        /// <returns>The room in requested direction. Null if invalid.</returns>
        public BlockController GetNeighbour(BlockController bc, Direction dir)
        {
            var (x, z) = BlockTransformToGrid(bc);
            return dir switch
            {
                Direction.Up => z + 1 < _generatorParams.size  ? _roomGrid[z + 1, x] : null,
                Direction.Right => x + 1 < _generatorParams.size  ? _roomGrid[z, x + 1] : null,
                Direction.Left => x - 1 >= 0 ? _roomGrid[z, x - 1] : null,
                Direction.Down => z - 1 >= 0 ? _roomGrid[z - 1, x] : null,
                _ => null
            };
        }
        /// <summary>
        /// Returns x and z of a block in the grid using its coordinates in game.
        /// Because the Coordinates start from 0,0 and all are multiplied by a size from generatorParams, this method is
        /// possible. 
        /// </summary>
        /// <param name="bc">Block controller of the block.</param>
        /// <returns>A tuple (x, z).</returns>
        private (int, int) BlockTransformToGrid(BlockController bc)
        {
            var x = (int)bc.transform.position.x;
            var z = (int)bc.transform.position.z;
            return (x / _generatorParams.scale, z / _generatorParams.scale);
        }
        /// <summary>
        /// Creates a random list of 4 directions
        /// </summary>
        /// <returns>Random list of directions.</returns>
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
        /// <summary>
        /// Given 2 neighbouring rooms, deletes the walls between them.
        /// </summary>
        /// <param name="b1">First block</param>
        /// <param name="b2">Second Block</param>
        public void DeleteWalls(BlockController b1, BlockController b2)
        {
            var bc1 = BlockTransformToGrid(b1);
            var bc2 = BlockTransformToGrid(b2);
            // Item1 = x, Item2 = z
            if (bc1.Item1 > bc2.Item1)
            {
                b1.ClearWall(Direction.Left);
                b2.ClearWall(Direction.Right);
                //Pillar Logic
                b1.GetPillar().DeactivateBool(Direction.Up);
                var up = GetNeighbour(b1, Direction.Up);
                if (up != null)
                    up.GetPillar().DeactivateBool(Direction.Down);
            }else if (bc1.Item1 < bc2.Item1)
            {
                b1.ClearWall(Direction.Right);
                b2.ClearWall(Direction.Left);
                //Pillar Logic
                b2.GetPillar().DeactivateBool(Direction.Up);
                var up = GetNeighbour(b2, Direction.Up);
                if (up != null)
                    up.GetPillar().DeactivateBool(Direction.Down);
            }else if (bc1.Item2 > bc2.Item2)
            {
                b1.ClearWall(Direction.Down);
                b2.ClearWall(Direction.Up);
                //Pillar Logic
                b1.GetPillar().DeactivateBool(Direction.Right);
                var right = GetNeighbour(b1, Direction.Right);
                if (right != null)
                    right.GetPillar().DeactivateBool(Direction.Left);
            }else if (bc1.Item2 < bc2.Item2)
            {
                b1.ClearWall(Direction.Up);
                b2.ClearWall(Direction.Down);
                //Pillar Logic
                b2.GetPillar().DeactivateBool(Direction.Right);
                var right = GetNeighbour(b2, Direction.Right);
                if (right != null)
                    right.GetPillar().DeactivateBool(Direction.Left);
            }
        }
    }
}