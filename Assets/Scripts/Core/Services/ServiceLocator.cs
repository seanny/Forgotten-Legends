using System;
using System.Collections.Generic;
using UnityEngine;
using Core.CommandConsole;

namespace Core.Services
{
    public static class ServiceLocator
    {
        public static List<IService> serviceList = new List<IService>();
        
#if UNITY_DEVELOPMENT || UNITY_EDITOR
        [RegisterCommand(Help = "Display help information about a command", MaxArgCount = 1)]
        private static void CommandServiceLocator(CommandArg[] args)
        {
            if (args.Length >= 1)
            {
                switch (args[0].String)
                {
                    case "list":
                        for (int i = 0; i < serviceList.Count; i++)
                        {
                            Terminal.Log($"Service #{i}: {serviceList[i].ToString()}");
                        }
                        break;
                }
            }
        }
#endif
        
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
                return AddService<T>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Cannot return type for {typeof(T).ToString()}: {e.Message}");
                throw;
            }
        }

        public static T AddService<T>()
        {
            IService service = (IService) Activator.CreateInstance(typeof(T));
            serviceList.Add(service);
            service.OnStart();
            return (T) service;
        }

        public static T AddService<T>(T serviceObject)
        {
            IService service = (IService) serviceObject;
            serviceList.Add(service);
            service.OnStart();
            return (T) service;
        }

        public static void EndAllServices()
        {
            foreach (var service in serviceList)
            {
                service.OnEnd();
            }
            serviceList.Clear();
        }
    }
}