using Core.Services;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[CustomEditor(typeof(SceneViewMouse))]
public class ProceduralPlacement : Editor
{
    
    
    
    void OnSceneGUI()
    {
        Debug.Log("OnSceneGUI");
        SceneViewMouse sceneViewMouse = (SceneViewMouse) target;
        if (sceneViewMouse == null)
        {
            return;
        }
        
        Camera camera = SceneView.lastActiveSceneView.camera;

        Vector3 mousePos = Event.current.mousePosition;
        mousePos.z = -camera.worldToCameraMatrix.MultiplyPoint(sceneViewMouse.transform.position).z;
        mousePos.y = Screen.height - mousePos.y - 36.0f;
        mousePos = camera.ScreenToWorldPoint(mousePos);

        sceneViewMouse.mousePositionWorld = mousePos;
        
        Gizmos.DrawSphere(mousePos, 10.0f);
    }
}