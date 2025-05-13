using System;
using Block;
using PCG.RoomData;
using UnityEngine;

namespace PCG
{
    public class Generator : MonoBehaviour
    {
        public GeneratorParams generatorParams;
        private BlockController[,] _roomGrid;

        private void SetupGrid()
        {
            _roomGrid = new BlockController[generatorParams.size, generatorParams.size];
            
            for (var i=0; i<generatorParams.size; i++)
            {
                for (var j=0; j<generatorParams.size; j++)
                {
                    var position = new Vector3(j * generatorParams.scale,
                        0,
                        i * generatorParams.scale);
                    _roomGrid[i, j] = Instantiate(generatorParams.blockPrefab, position, Quaternion.identity).
                                      GetComponent<BlockController>();
                }
            }
        }

        private void ClearRooms(int x, int z, int xSize, int zSize)
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
        private void PlaceRoom(int x, int z, BaseRoom rd)
        {
            ClearRooms(x,z,rd.width, rd.height);
            // Opening a door to the newly created room
            _roomGrid[z + rd.doorZ, x + rd.doorX].EnableDoor(rd.doorDirection);
            // Adding 
            rd.Setup(new Vector3(x * generatorParams.scale, 0, z * generatorParams.scale));
        }
        private void Generate()
        {
            SetupGrid();
            PlaceRoom(2,2, generatorParams.safeRoomData);
        }

        private void Start()
        {
            Generate();
        }
    }
}
