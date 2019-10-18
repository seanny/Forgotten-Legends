using System;
using UnityEngine;

namespace Core.Swimming
{
    public class ActorSwimmingController : MonoBehaviour
    {
        private Actor.Actor m_Actor;

        private void Start()
        {
            m_Actor = GetComponent<Actor.Actor>();
        }

        public void ToggleSwimmingAnimation(bool toggle)
        {
            m_Actor.animationController.SetSwim(toggle);
        }
    }
}