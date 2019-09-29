#if UNITY_EDITOR
using System.IO;
using Core.MathUtil;
using Core.MeshLoading;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public class MeshPackaging : EditorWindow
    {
        private Object m_GameObject;
        private Object m_Texture;
        private Object m_Normal;
        private Object m_AmbientOcclusion;
        private bool m_IsWalkable;
        private MetaLight m_Light;
        private MetaInteractable m_Interactable;
        private MetaRigidbody m_Rigidbody;
        private MetaCollider m_Collider;
        private MetaTimed m_Timed;
        private float m_DrawDistance;
        private string m_ObjectName;

        private Object m_ReferenceLight;
        private Object m_ReferenceRigidbody;
        private Object m_ReferenceCollider;
        private Object m_ReferenceTimed;
        private Object m_ReferenceInteractable;
        
        [MenuItem("Tools/Mesh Packaging Utility")]
        private static void ShowWindow()
        {
            var window = GetWindow<MeshPackaging>();
            window.titleContent = new GUIContent("Mesh Packaging Utility");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Mesh and Textures (Required)");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Mesh");
            m_GameObject = EditorGUILayout.ObjectField("Mesh", m_GameObject, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Diffuse (D)");
            m_Texture = EditorGUILayout.ObjectField(m_Texture, typeof(Texture2D), false);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Normal (N)");
            m_Normal = EditorGUILayout.ObjectField(m_Normal, typeof(Texture2D), false);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Occlusion (AO)");
            m_AmbientOcclusion = EditorGUILayout.ObjectField(m_AmbientOcclusion, typeof(Texture2D), false);
            EditorGUILayout.EndHorizontal();
            
            m_IsWalkable = EditorGUILayout.Toggle("AI Walkable", m_IsWalkable);
            m_DrawDistance = EditorGUILayout.FloatField("Draw Distance", m_DrawDistance);

            GUILayout.Label("Lighting, Rigidbody and Collider (Optional)");
            m_ReferenceLight = EditorGUILayout.ObjectField(m_ReferenceLight, typeof(Light), true);
            m_ReferenceRigidbody = EditorGUILayout.ObjectField(m_ReferenceRigidbody, typeof(Rigidbody), true);
            m_ReferenceCollider = EditorGUILayout.ObjectField(m_ReferenceCollider, typeof(MeshCollider), true);
            m_ReferenceInteractable = EditorGUILayout.ObjectField(m_ReferenceInteractable, typeof(Interactable.Interactable), true);

            if (GUILayout.Button("Package Mesh"))
            {
                PackageMesh();
            }
        }

        private void PackageMesh()
        {
            string meshObj = Path.GetFileName(AssetDatabase.GetAssetPath(m_GameObject));
            string meshLocation = AssetDatabase.GetAssetPath(m_GameObject);
            if (meshObj == null)
            {
                Debug.LogError("You must assign a mesh.");
                return;
            }
            
            string folderName = Path.ChangeExtension(meshObj, null);
            string metaFile = Path.ChangeExtension(meshObj, ".json");
            
            string diffusePNG = Path.GetFileName(AssetDatabase.GetAssetPath(m_Texture));
            string diffuseLocation = AssetDatabase.GetAssetPath(m_Texture);
            if (diffusePNG == null)
            {
                Debug.LogError("You must assign a texture.");
                return;
            }

            string normalPNG = Path.GetFileName(AssetDatabase.GetAssetPath(m_Normal));
            string normalLocation = AssetDatabase.GetAssetPath(m_Normal);
            if (normalPNG == null)
            {
                Debug.LogError("You must assign a normal map.");
                return;
            }
            
            string occlusionPNG = Path.GetFileName(AssetDatabase.GetAssetPath(m_AmbientOcclusion));
            string occlusionLocation = AssetDatabase.GetAssetPath(m_AmbientOcclusion);
            if (occlusionPNG == null)
            {
                Debug.LogError("You must assign an occlusion map.");
                return;
            }

            m_Interactable = new MetaInteractable();
            m_Interactable.interactableType = m_Interactable.interactableType;
            m_Interactable.isInteractable = m_Interactable.isInteractable;
            
            Light light;
            if (m_ReferenceLight != null)
            {
                light = (Light) m_ReferenceLight;
            }
            else light = new Light();
            
            m_Light = new MetaLight();
            if (m_ReferenceLight != null)
            {
                m_Light.isEnabled = true;
                m_Light.angle = light.spotAngle;
                m_Light.range = light.range;
                m_Light.colour = new Colour(light.color);
                m_Light.intensity = light.intensity;
                m_Light.isSpotlight = light.type == LightType.Spot;
            }
            else m_Light.isEnabled = false;
            
            Rigidbody rigidbody;
            if (m_ReferenceLight != null)
            {
                rigidbody = (Rigidbody) m_ReferenceRigidbody;
            }
            else rigidbody = new Rigidbody();

            m_Rigidbody = new MetaRigidbody();
            if (m_ReferenceRigidbody != null)
            {
                m_Rigidbody.isEnabled = true;
                m_Rigidbody.drag = rigidbody.drag;
                m_Rigidbody.mass = rigidbody.mass;
                m_Rigidbody.position = new Vec3(rigidbody.position);
                m_Rigidbody.rotation = new Quat(rigidbody.rotation);
            }
            else m_Rigidbody.isEnabled = false;

            MeshCollider meshCollider;
            if (m_ReferenceCollider != null)
            {
                meshCollider = (MeshCollider) m_ReferenceCollider;
            }
            else meshCollider = new MeshCollider();

            m_Collider = new MetaCollider();
            if (m_ReferenceCollider != null)
            {
                m_Collider.isEnabled = true;
                m_Collider.isTrigger = meshCollider.isTrigger;
            }
            else m_Collider.isEnabled = false;

            ObjectMetaFile objectMetaFile = new ObjectMetaFile
            {
                meshObj = $"{folderName}/{meshObj}",
                texturePng = $"{folderName}/{diffusePNG}",
                normalPng = $"{folderName}/{normalPNG}",
                ambientOcclusionPng = $"{folderName}/{occlusionPNG}",
                objectName = m_GameObject.name,
                collider = m_Collider,
                drawDistance = m_DrawDistance,
                heightMapPng = null,
                interactable = m_Interactable,
                isWalkable = m_IsWalkable,
                light = m_Light,
                rigidbody = m_Rigidbody,
            };

            string folderPath = Path.Combine(Application.streamingAssetsPath, "Models", folderName);
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
            
            Directory.CreateDirectory(folderPath);
            
            string json = JsonUtility.ToJson(objectMetaFile, true);
            Debug.Log($"{Path.Combine(folderPath, metaFile)}");
            File.WriteAllText(Path.Combine(folderPath, metaFile), json);
            File.Copy(meshLocation, Path.Combine(folderPath, meshObj));
            File.Copy(diffuseLocation, Path.Combine(folderPath, diffusePNG));
            File.Copy(normalLocation, Path.Combine(folderPath, normalPNG));
            File.Copy(occlusionLocation, Path.Combine(folderPath, occlusionPNG));
        }
    }
}
#endif