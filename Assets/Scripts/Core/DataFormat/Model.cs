using System;

namespace Core.DataFormat
{
    [Serializable]
    public class Model : Record
    {
        /// <summary>
        /// .obj File Path
        /// </summary>
        public string mesh;
        
        /// <summary>
        /// Diffuse Texture File Path
        /// </summary>
        public string texture;
        
        /// <summary>
        /// Normal Map File Path
        /// </summary>
        public string normal;
        
        /// <summary>
        /// Ambient Occlusion File Path
        /// </summary>
        public string ambientOcclusion;
        
        /// <summary>
        /// Height Map File Path
        /// </summary>
        public string heightMap;
    }
}