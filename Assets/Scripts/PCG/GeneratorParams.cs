using System.Collections.Generic;
using Block;
using PCG.RoomData;
using UnityEngine;

namespace PCG
{
    [CreateAssetMenu(fileName = "GeneratorParams", menuName = "PCG/GeneratorParams")]
    public class GeneratorParams : ScriptableObject
    {
        // General Parameters
        public int seed;
        public int size;
        public int padding;
        
        // Block Data
        public GameObject blockPrefab;
        public int        scale;
        
        // Room generation Assets
        public List<RoomDataPair> roomDataList;
    }
}