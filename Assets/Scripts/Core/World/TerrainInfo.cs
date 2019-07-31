using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Core.Utility;
using UnityEngine;

namespace Core.World
{
    [Serializable]
    public class TerrainInfo
    {
        public TerrainInfo(TerrainData terrainData)
        {
            Debug.Log("Saving terrain...");
            heightmapWidth = terrainData.heightmapHeight;
            heightmapHeight = terrainData.heightmapHeight;
            heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
            terrainSize = new TerrainSize(terrainData.size);
            heightmapResolution = terrainData.heightmapResolution;
            terrainTextures = new List<TerrainTexture>();
            Debug.Log($"{terrainData.terrainLayers.Length}");
            for (int i = 1; i < terrainData.terrainLayers.Length; i++)
            {
                ImageUtils.GetImageSize(terrainData.terrainLayers[i].diffuseTexture, out int w, out int h);
                TerrainTexture terrainTexture = new TerrainTexture();
                Debug.Log($"Info: {w}, {h}, {terrainData.terrainLayers[i].diffuseTexture.name}");

                terrainTexture.diffuseTexture = terrainData.terrainLayers[i].diffuseTexture.EncodeToPNG();
                terrainTexture.width = w;
                terrainTexture.height = h;
                terrainTextures.Add(terrainTexture);
            }
            Debug.Log("Terrain saved.");
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
        public byte[] diffuseTexture;

        public int width;

        public int height;
        //public string normalTexture;
        
        public float metallic;
        public float smoothness;
        public float tileOffsetX;
        public float tileOffsetY;
        public float tileSizeX;
        public float tileSizeY;
    }
}