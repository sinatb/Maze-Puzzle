using System.Collections;
using System.Collections.Generic;
using Block;
using PCG.RoomData;
using UnityEngine;
using Util;

namespace PCG
{
    public class Generator : MonoBehaviour
    {
        public GeneratorParams     generatorParams;
        private BlockController[,] _roomGrid;
        private GeneratorUtil      _util;
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
        private void ChooseRoomPosition()
        {
            foreach (var roomData in generatorParams.roomDataList)
            {
                var roomCount = roomData.roomCount;
                var roomDataObj = roomData.roomData;
                var padding = generatorParams.padding;
                for (var i = 0; i < roomCount; i++)
                {
                    var x = Random.Range(padding, generatorParams.size-padding);
                    var z = Random.Range(padding, generatorParams.size-padding);
                    while (!_util.CheckRoomPosition(x, z, roomDataObj.width, roomDataObj.height))
                    {
                        x = Random.Range(padding, generatorParams.size-padding);
                        z = Random.Range(padding, generatorParams.size-padding);
                    }
                    PlaceRoom(x,z,roomDataObj);
                }
            }
        }
        private void PlaceRoom(int x, int z, BaseRoom rd)
        {
            _util.ClearRooms(x,z,rd.width, rd.height);
            _util.SetBlockType(x,z,rd.width,rd.height,rd.blockType);
            // Opening a door to the newly created room
            _util.CreateDoor(x + rd.doorX,z + rd.doorZ, rd.doorDirection);
            // Adding 
            rd.Setup(new Vector3(x * generatorParams.scale, 0, z * generatorParams.scale));
        }
        private void CreateCorridors(BlockController bc, List<BlockController> path)
        {
            Debug.Log(bc.transform.position);
            bc.SetVisited();
            var directions = _util.GetRandomDirectionList();
            foreach (var direction in directions)
            {
                var nb = _util.GetNeighbour(bc,direction);
                if (nb != null && !nb.IsVisited() && nb.IsCorridor())
                {
                    _util.DeleteWalls(bc,nb);
                    path.Add(nb);
                    CreateCorridors(nb,path);
                }
            }
            if (path.Count == 1)
                return;
            path.RemoveAt(path.Count-1);
            CreateCorridors(path[^1],path);
        }
        private void Generate()
        {
            SetupGrid();
            _util = new GeneratorUtil(_roomGrid, generatorParams);
            ChooseRoomPosition();
            var path = new List<BlockController>() {_roomGrid[0,0]};
            CreateCorridors(_roomGrid[0,0], path);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.05f);
            Generate();
        }
    }
}
