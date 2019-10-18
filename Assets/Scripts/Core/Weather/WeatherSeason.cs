using System;
using Core.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Weather
{
    public class WeatherSeason : IService
    {
        public WeatherData weatherData { get; private set; } 

        private void Start()
        {
            weatherData = new WeatherData();
        }

        public void SetWeather(WeatherType weatherType)
        {
            weatherData.CurrentWeather = weatherType;
        }
        
        public int SetRandomWeather()
        {
            int weatherChance =
                Random.Range(0, Enum.GetNames(typeof(WeatherType)).Length);
            SetWeather((WeatherType) weatherChance);
            return weatherChance;
        }

        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }
    }
}