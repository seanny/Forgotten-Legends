using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace Core.World
{
    public class TerrainManager : Singleton<TerrainManager>
    {
        public Terrain terrain;
        private TerrainData m_TerrainData;

        public bool IsLoaded { get; private set; }

        private void Start()
        {
            m_TerrainData = terrain.terrainData;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                SaveTerrain("Test.trrn");
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                LoadTerrain("Test.trrn");
            }
        }

        private void SaveTerrain(string filename)
        {
            TerrainInfo terrainInfo = new TerrainInfo(m_TerrainData);

            string finalPath = Path.Combine(Application.streamingAssetsPath, "Terrain", filename);
            
            FileStream fs = new FileStream(finalPath, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fs, terrainInfo);
            fs.Close();
        }

        /// <summary>
        /// Load a file ending with .trrn, this should be hidden behind a loading screen as it's extremely slow.
        /// </summary>
        /// <param name="filename"></param>
        public void LoadTerrain(string filename)
        {
            string finalPath = Path.Combine(Application.streamingAssetsPath, "Terrain", filename);
            if (!File.Exists(finalPath))
            {
                Debug.LogError($"Error: {filename} does not exist.");
                return;
            }
            StartCoroutine(InternalLoadTerrain(finalPath));
        }
        
        private IEnumerator InternalLoadTerrain(string finalPath)
        {
            Debug.Log($"Loading terrain {finalPath}...");
            IsLoaded = false;
            FileStream fs = new FileStream(finalPath, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            TerrainInfo terrainInfo = (TerrainInfo)binaryFormatter.Deserialize(fs);
            fs.Close();
            
            float[,] dat = m_TerrainData.GetHeights(0,0,terrainInfo.heightmapHeight,terrainInfo.heightmapWidth);
            for(int i = 0; i < terrainInfo.heightmapWidth; i++)
            {
                for(int j = 0; j < terrainInfo.heightmapHeight; j++)
                {
                    dat[i, j] = (float)terrainInfo.heights[i, j];
                }
                yield return null;
            }
            
            SplatPrototype[] terrainTextures = new SplatPrototype[terrainInfo.terrainTextures.Count];
            for(int i = 0; i < terrainInfo.terrainTextures.Count; i++)
            {
                terrainTextures[i] = new SplatPrototype();
                Texture2D texture2D = new Texture2D(terrainInfo.terrainTextures[i].width, terrainInfo.terrainTextures[i].height);
                texture2D.LoadImage(terrainInfo.terrainTextures[i].diffuseTexture);
                texture2D.Apply();
                terrainTextures[i].texture = texture2D;
                yield return null;
            }
            
            m_TerrainData.size = new Vector3(terrainInfo.terrainSize.x, terrainInfo.terrainSize.y, terrainInfo.terrainSize.z);
            m_TerrainData.SetHeights(0,0,dat);
            m_TerrainData.splatPrototypes = terrainTextures;

            /*m_TerrainData.terrainLayers = new TerrainLayer[terrainTextures.Length];
            Debug.Log($"Terrain Layers: {m_TerrainData.terrainLayers.Length}");
            for (int i = 0; i < m_TerrainData.terrainLayers.Length; i++)
            {
                Texture2D diffuse = new Texture2D(terrainInfo.terrainTextures[i].width, terrainInfo.terrainTextures[i].height);
                if (diffuse.LoadImage(terrainInfo.terrainTextures[i].diffuseTexture) == true)
                {
                    Debug.Log($"Terrain Layers: {m_TerrainData.terrainLayers.Length}");
                    diffuse.Apply();
                    m_TerrainData.terrainLayers[i].diffuseTexture = diffuse;
                }
                else
                {
                    Debug.Log($"Cannot load PNG data");
                }
            }
            
            float[,,] map = new float[m_TerrainData.alphamapWidth, m_TerrainData.alphamapHeight, 2];
            
            // For each point on the alphamap...
            for (int y = 0; y < m_TerrainData.alphamapHeight; y++)
            {
                for (int x = 0; x < m_TerrainData.alphamapWidth; x++)
                {
                    // Get the normalized terrain coordinate that
                    // corresponds to the the point.
                    float normX = x * 1.0f / (m_TerrainData.alphamapWidth - 1);
                    float normY = y * 1.0f / (m_TerrainData.alphamapHeight - 1);

                    // Get the steepness value at the normalized coordinate.
                    var angle = m_TerrainData.GetSteepness(normX, normY);

                    // Steepness is given as an angle, 0..90 degrees. Divide
                    // by 90 to get an alpha blending value in the range 0..1.
                    var frac = angle / 90.0;
                    map[x, y, 0] = (float)frac;
                    map[x, y, 1] = (float)(1 - frac);
                }
            }
            m_TerrainData.SetAlphamaps(0, 0, map);*/
            
            IsLoaded = true;
            Debug.Log($"Terrain {finalPath} loaded");
        }
    }
}