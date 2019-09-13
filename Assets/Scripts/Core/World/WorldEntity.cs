using UnityEngine;
using Core.CommandConsole;
using Core.Utility;

public class WorldEntity : MonoBehaviour
{
    public string entityID;

    [RegisterCommand(Help =  "Get Entity ID")]
    public static string GetEntityID(string gameObjectName)
    {
        WorldEntity[] worldEntities = FindObjectsOfType<WorldEntity>();
        foreach (var entity in worldEntities)
        {
            if (entity.name == gameObjectName)
            {
                return entity.entityID;
            }
        }
        return null;
    }
    
    public static WorldEntity FindEntity(string entityID)
    {
        WorldEntity[] worldEntities = FindObjectsOfType<WorldEntity>();
        foreach (var entity in worldEntities)
        {
            if (entity.entityID == entityID)
            {
                return entity;
            }
        }
        return null;
    }
    
    protected virtual void Start()
    {
        entityID = Crypto.GenerateKey();
        Debug.Log($"{gameObject.name} ID: {entityID}");
    }
}
