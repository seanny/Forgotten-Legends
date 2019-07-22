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
            terrainSize = new TerrainSize(terrainData.size);
            heightmapResolution = terrainData.heightmapResolution;
        }

        public int heightmapWidth;
        public int heightmapHeight;
        public int heightmapResolution;
        public float[,] heights;
        public TerrainSize terrainSize;
        public List<TerrainTexture> terrainTextures;

    }

    [Serializable]
    public class TerrainSize
    {
        public TerrainSize(Vector3 size)
        {
            x = size.x;
            y = size.y;
            z = size.z;
        }
        
        public float x;
        public float y;
        public float z;
        
    }

    [Serializable]
    public class TerrainTexture
    {
        public string diffuseTexture;
        
        public float smoothness;
        public float tileOffsetX;
        public float tileOffsetY;
        public float tileSizeX;
        public float tileSizeY;
    }
}