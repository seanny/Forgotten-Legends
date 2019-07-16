using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.World
{
    [Serializable]
    public class TerrainInfo
    {
        public TerrainInfo(TerrainData terrainData)
        {
            heightmapWidth = terrainData.heightmapHeight;
            heightmapHeight = terrainData.heightmapHeight;
            heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
        }

        public int heightmapWidth;
        public int heightmapHeight;
        public float[,] heights;

    }
}