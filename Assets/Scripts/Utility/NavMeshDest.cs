using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshDest : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
