using System;
using System.Collections.Generic;
using Core.Factions;
using Core.Interactable;
using Core.SaveFile;
using Core.World;
using UnityEngine;

namespace Core.Services
{
    /// <summary>
    /// Class to automatically start certain services on scene load.
    /// </summary>
    public class StartupServices : MonoBehaviour
    {
        public bool isLoaded { get; private set; }
        
        private void Start()
        {
            LoadServices();
        }

        private void LoadServices()
        {
            isLoaded = false;
            ServiceLocator.GetService<SaveService>();
            ServiceLocator.GetService<InteractableGUI>();
            ServiceLocator.GetService<WorldspaceManager>();
            ServiceLocator.GetService<CoreFactions>();
            isLoaded = true;
        }
    }
}