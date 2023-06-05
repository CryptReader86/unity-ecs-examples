using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    [SerializeField]
    public GameObject _planetPrefab;

    private const int NumPlanets = 500;

    void Start()
    {
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_planetPrefab, settings);

        for (int i = 0; i < NumPlanets; i++)
        {
            var instance = manager.Instantiate(prefab);

            float x = UnityEngine.Random.Range(-100, 100);
            float z = UnityEngine.Random.Range(-100, 100);
            var position = transform.TransformPoint(new float3(x, 0, z));

            manager.SetComponentData(instance, new Translation { Value = position });
        }
    }
}
