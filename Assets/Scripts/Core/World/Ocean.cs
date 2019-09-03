using UnityEngine;

namespace Core.World
{
    public class Ocean : Singleton<Ocean>
    {
        public Transform waterParent;
        public GameObject waterPrefab;
        public float x_Start, y_Start;
        public int columnLength, rowLength;
        public float x_Space, y_Space;

        private void Start()
        {
            GenerateWater();
        }

        private void GenerateWater()
        {
            for (int i = 0; i < columnLength * rowLength; i++)
            {
                GameObject gameObject = Instantiate(waterPrefab, new Vector3(x_Start + x_Space + (x_Space * (i % columnLength)), 0.0f, y_Start + y_Space * (i / columnLength)), Quaternion.identity);
                gameObject.transform.parent = waterParent;
            }
        }
    }
}