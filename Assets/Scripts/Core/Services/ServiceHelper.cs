using System;
using UnityEngine;

namespace Core.Services
{
    public class ServiceHelper : Singleton<ServiceHelper>
    {
        public Action onUpdate;

        private void Update()
        {
            if (onUpdate != null)
            {
                onUpdate();
            }
        }
    }
}