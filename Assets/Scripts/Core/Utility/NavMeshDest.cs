using UnityEngine;

namespace Core.Utility
{
    public class NavMeshDest : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}
