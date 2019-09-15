using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using Core.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.World
{
    public class CloudManager : MonoBehaviour, IService
    {
        private GameObject[] cloudPrefabs;
        private Transform parentTransform;

        private float m_NextActionTime = 0.0f;
        private float m_WaitTime = 0f;
        private bool m_IsEnabled;
        
        private void OnCloudSpawn()
        {
            int index = Random.Range(0, cloudPrefabs.Length - 1);
            float _scale = Random.Range(1f, 5f);
            GameObject _cloud = Instantiate(cloudPrefabs[index].gameObject, parentTransform, true);
            _cloud.transform.position = new Vector3(Random.Range(-4000f, 4000f), Random.Range(100f, 500f),
                Random.Range(-4000f, 4000f));
            _cloud.transform.localScale = new Vector3(_scale, _scale, _scale);
        }

        public void OnStart()
        {
            
        }

        private void Update()
        {
            if (m_IsEnabled)
            {
                m_WaitTime += Time.deltaTime;
                if(m_WaitTime >= 1.0f)
                {
                    m_WaitTime = 0;
                    OnCloudSpawn();
                }
            }
        }

        public void ToggleCloudGeneration(bool toggle)
        {
            m_IsEnabled = toggle;
        }
        
        private void AddClouds()
        {
            parentTransform = GameObject.Find("_Dynamic").transform;
            cloudPrefabs = Resources.LoadAll<GameObject>("Clouds");
            ToggleCloudGeneration(true);
        }

        private void Start()
        {
            AddClouds();
            ServiceLocator.AddService(this);
        }

        public void OnEnd()
        {
            
        }
    }
}