using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Core.DataFormat
{
    public class OGSDReader
    {
        public OGSDFile dataFile
        {
            get
            {
                return m_DataFile;
            }
            private set { m_DataFile = value; }
        }
        
        [SerializeField]
        private OGSDFile m_DataFile;
        
        public void Open(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"[OGSDReader]: {Path.GetFileName(filePath)} does not exist.");
                return;
            }
            
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);
            m_DataFile = (OGSDFile) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }

        public void Close()
        {
            m_DataFile = null;
        }

        public float GetVersion()
        {
            return m_DataFile.dataHeader.version;
        }
        
        public string GetAuthor()
        {
            return m_DataFile.dataHeader.author;
        }
        
        public string GetDescription()
        {
            return m_DataFile.dataHeader.description;
        }
    }
}