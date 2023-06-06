using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    EntityManager manager;
    public GameObject asteroidPrefab;
    const int numAsteroids = 5000;

    void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidPrefab, settings);

        for (int i = 0; i < numAsteroids; i++)
        {
            var instance = manager.Instantiate(prefab);
            float x = Mathf.Sin(i) * UnityEngine.Random.Range(25, 50);
            float z = Mathf.Cos(i) * UnityEngine.Random.Range(25, 50);
            var position = transform.TransformPoint(new float3(x, UnityEngine.Random.Range(-2.0f, 2.0f), z));
            manager.SetComponentData(instance, new Translation { Value = position });

            var q = Quaternion.Euler(new Vector3(0, 0, 0));
            manager.SetComponentData(instance, new Rotation { Value = new quaternion(q.x,q.y,q.z,q.w) });

            var scale = UnityEngine.Random.Range(25.0f, 100.0f);
            manager.SetComponentData(instance, new NonUniformScale { Value = new float3(scale) });
        }

    }
}
