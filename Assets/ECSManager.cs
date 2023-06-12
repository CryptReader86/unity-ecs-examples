using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    EntityManager manager;
    public GameObject shipPrefab;
    const int numShips = 500;

    void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(shipPrefab, settings);

        for (int i = 0; i < numShips; i++)
        {
            var instance = manager.Instantiate(prefab);
            float x = UnityEngine.Random.Range(-300, 300);
            float y = UnityEngine.Random.Range(-300, 300);
            float z = UnityEngine.Random.Range(-300, 300);
            var position = transform.TransformPoint(new float3(x, y, z));
            manager.SetComponentData(instance, new Translation { Value = position });

            var q = Quaternion.Euler(new Vector3(0, 45, 0));
            manager.SetComponentData(instance, new Rotation { Value = new quaternion(q.x,q.y,q.z,q.w) });

            var closestWP = 0;
            var minDistance = float.MaxValue;
            for(var j = 0; j < GameDataManager.instance.wps.Length; j++)
            {
                var distance = Vector3.Distance(position, GameDataManager.instance.wps[j]);
                if (distance <= minDistance)
                {
                    closestWP = j;
                    minDistance = distance;
                }
            }

            manager.SetComponentData(instance, new ShipData { speed = UnityEngine.Random.Range(5, 20),
                rotationSpeed = UnityEngine.Random.Range(3, 5),
                currentWP = closestWP
            });
        }

    }
}
