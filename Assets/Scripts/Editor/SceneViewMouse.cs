using UnityEngine;

[ExecuteInEditMode]
public class SceneViewMouse : MonoBehaviour
{
    public Vector3 mousePositionWorld;
    
    private void OnDrawGizmos()
    {
        Debug.Log($"{mousePositionWorld}");
        Gizmos.DrawLine(this.transform.position, mousePositionWorld);
        //Gizmos.DrawSphere(mousePositionWorld, 5f);
    }
}