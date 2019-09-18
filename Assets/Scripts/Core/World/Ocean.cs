using System.Collections.Generic;
using Core.Services;
using UnityEngine;

namespace Core.World
{
    public class Ocean : IService
    {
        public Transform waterParent;
        public GameObject waterPrefab;
        public List<GameObject> waterTiles = new List<GameObject>();
        
        private float x_Start = 0; 
        private float y_Start = 0;
        private int columnLength = 500;
        private int rowLength = 500;
        private float x_Space = 10; 
        private float y_Space = 10;

        public void GenerateWater(int columns, int rows)
        {
            RemoveWater();
            columnLength = columns;
            rowLength = rows;
            
            LoadOceanPrefabAndAssignParent();
            for (int i = 0; i < columnLength * rowLength; i++)
            {
                GameObject gameObject = Object.Instantiate(waterPrefab, new Vector3(x_Start + x_Space + (x_Space * (i % columnLength)), 0.0f, y_Start + y_Space * (i / columnLength)), Quaternion.identity);
                gameObject.transform.parent = waterParent;
                waterTiles.Add(gameObject);
            }
            Debug.Log($"Generated water with {columnLength} columns and {rowLength} rows.");
        }

        public void RemoveWater()
        {
            foreach (var tile in waterTiles)
            {
                GameObject.Destroy(tile);
            }
        }

        private void LoadOceanPrefabAndAssignParent()
        {
            if (!waterPrefab)
            {
                waterPrefab = Resources.Load<GameObject>("Ocean");
                waterParent = GameObject.Find("_Dynamic").transform;
            }
        }

        public void OnStart()
        {
            GenerateWater(500, 500);
        }

        public void OnEnd()
        {
            
        }
    }
}