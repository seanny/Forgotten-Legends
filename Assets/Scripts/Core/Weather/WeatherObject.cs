using System;
using System.Collections.Generic;
using Core.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Weather
{
    public class WeatherObject : MonoBehaviour
    {
        [SerializeField] private float m_WeatherTime = 0f;
        [SerializeField] private float m_WeatherUpdateTime;
        private const float MIN_UPDATE_TIME = 600f;
        private const float MAX_UPDATE_TIME = 3600f;

        public List<GameObject> weatherPrefabs;
        public GameObject weatherObject;
        
        private void Start()
        {
            ServiceLocator.GetService<WeatherSeason>().SetRandomWeather();
            m_WeatherUpdateTime = Random.Range(MIN_UPDATE_TIME, MAX_UPDATE_TIME);
        }

        private void Update()
        {
            m_WeatherTime += Time.deltaTime;
            if (m_WeatherTime >= m_WeatherUpdateTime)
            {
                int weather = ServiceLocator.GetService<WeatherSeason>().SetRandomWeather();
                if (weatherObject != null)
                {
                    GameObject.Destroy(weatherObject);
                }

                weatherObject = GameObject.Instantiate(weatherPrefabs[weather]);
                weatherObject.transform.localPosition = Vector3.zero;
                weatherObject.transform.localScale = Vector3.one;
                weatherObject.transform.localRotation = Quaternion.identity;
            }
        }
    }
}