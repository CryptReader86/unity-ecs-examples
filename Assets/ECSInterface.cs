using UnityEngine;
using Unity.Entities;

public class ECSInterface : MonoBehaviour
{
    private void Start()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        var entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;

        var entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<SheepData>());
        Debug.Log("Sheep count: " + entityQuery.CalculateEntityCount());
    }
}
