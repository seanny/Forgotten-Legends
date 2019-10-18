using System.Collections.Generic;
using UnityEngine;

namespace Core.Swimming
{
    public class SwimmingObject : MonoBehaviour
    {
        [SerializeField] private SwimmingObjectData swimmingObjectData;

        private void Start()
        {
            swimmingObjectData = new SwimmingObjectData();
            swimmingObjectData.collider = gameObject.AddComponent<BoxCollider>();
            swimmingObjectData.actors = new List<Actor.Actor>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Actor.Actor actor = null;
            gameObject.TryGetComponent<Actor.Actor>(out actor);
            if (actor != null)
            {
                swimmingObjectData.actors.Add(actor);
                actor.actorSwimmingController.ToggleSwimmingAnimation(true);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            Actor.Actor actor = null;
            gameObject.TryGetComponent<Actor.Actor>(out actor);
            if(swimmingObjectData.actors.Contains(actor))
            {
                swimmingObjectData.actors.Remove(actor);
                actor.actorSwimmingController.ToggleSwimmingAnimation(false);
            }
        }
    }
}