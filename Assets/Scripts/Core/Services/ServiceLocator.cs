using System;
using System.Collections.Generic;
using Core.Localisation;
using UnityEngine;

namespace Core.Services
{
    public static class ServiceLocator
    {
        public static List<IService> serviceList = new List<IService>();

        public static T GetService<T>()
        {
            foreach (var service in serviceList)
            {
                if (service.GetType() == typeof(T))
                {
                    return (T)service;
                }
            }

            try
            {
                IService service = (IService) Activator.CreateInstance(typeof(T));
                serviceList.Add(service);
                return (T)service;
            }
            catch (Exception e)
            {
                Debug.LogError($"Cannot return type: {e.Message}");
                throw;
            }
        }
    }
}