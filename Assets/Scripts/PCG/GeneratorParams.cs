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
        
        // Block Data
        public GameObject blockPrefab;
        public int        scale;
        
        // Room generation Parameters
        public int safeRoomCount;
        public int supplyRoomCount;
        public int generatorRoomCount;
        public int workshopRoomCount;
        
        // Room generation Assets
        public BaseRoom safeRoomData;
        public BaseRoom supplyRoomData;

    }
}