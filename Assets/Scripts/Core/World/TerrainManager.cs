using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Core.World
{
    public class TerrainManager : Singleton<TerrainManager>
    {
        public Terrain terrain;
        private TerrainData m_TerrainData;
        private float[,] heights;

        public List<string> terrainTextures;
        public bool isLoaded { get; private set; }

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

            /*float[,] dat = m_TerrainData.GetHeights(0,0,m_TerrainData.heightmapWidth,m_TerrainData.heightmapHeight);             
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            for(int i = 0; i < m_TerrainData.heightmapWidth; i++) 
            {
                for(int j = 0; j < m_TerrainData.heightmapHeight; j++) 
                {
                    bw.Write(dat[i,j]);
                }
            }
            bw.Close();*/
        }

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
            isLoaded = false;
            FileStream fs = new FileStream(finalPath, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            TerrainInfo terrainInfo = (TerrainInfo)binaryFormatter.Deserialize(fs);
            fs.Close();

            Debug.Log($"Reading terrain info ({terrainInfo.heightmapHeight}, {terrainInfo.heightmapWidth})...");
            
            float[,] dat = m_TerrainData.GetHeights(0,0,terrainInfo.heightmapHeight,terrainInfo.heightmapWidth);
            for(int i = 0; i < terrainInfo.heightmapWidth; i++)
            {
                for(int j = 0; j < terrainInfo.heightmapHeight; j++)
                {
                    dat[i, j] = (float) terrainInfo.heights[i, j];
                }
                yield return null;
            }

            Debug.Log($"Finished reading terrain info.");
            m_TerrainData.SetHeights(0,0,dat);
            heights = m_TerrainData.GetHeights(50,50,100,100);

            /*float[,] dat = m_TerrainData.GetHeights(0,0,m_TerrainData.heightmapWidth,m_TerrainData.heightmapHeight);
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(fs);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            for(int i = 0; i < m_TerrainData.heightmapWidth; i++)
            {
                for(int j = 0; j < m_TerrainData.heightmapHeight; j++)
                {
                    dat[i,j] = (float)br.ReadSingle();
                }
            }
            br.Close();
            m_TerrainData.SetHeights(0,0,dat);
            heights = m_TerrainData.GetHeights(50,50,100,100);*/
            isLoaded = true;
        }
    }
}