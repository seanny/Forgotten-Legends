using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Core.DataFormat
{
    /// <summary>
    /// Class for reading and writing OGSD files
    /// OGSD = Outlaw Games Studio Data
    /// </summary>
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

        public void Write(string filePath)
        {
            if (File.Exists(filePath))
            {
                Debug.LogWarning($"Overwriting file {filePath}...");
                File.Delete(filePath);
            }
            else
            {
                Debug.Log($"Writing file {filePath}...");
            }

            FileStream fileStream = File.Open(filePath, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(fileStream, dataFile);
            }
            catch (SerializationException error)
            {
                Debug.LogError($"[OGSDReader]: {Path.GetFileName(filePath)} cannot be serialised: {error.Message}");
            }
            finally
            {
                fileStream.Close();
            }
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