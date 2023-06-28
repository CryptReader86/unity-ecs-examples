using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    [SerializeField] private GameObject _sandPrefab;
    [SerializeField] private GameObject _dirtPrefab;
    [SerializeField] private GameObject _grassPrefab;
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private GameObject _snowPrefab;

    [Range(0.1f, 10.0f)]
    [SerializeField] private float _strength = 1.0f;
    [Range(0.01f, 1.0f)]
    [SerializeField] private float _scale = 0.1f;

    private const int WorldHalfSize = 75;

    private void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_sandPrefab, settings);

        for (var z = -WorldHalfSize; z <= WorldHalfSize; z++)
        {
            for (var x = -WorldHalfSize; x <= WorldHalfSize; x++)
            {
                var position = new float3(x, 0, z);
                var entity = entityManager.Instantiate(prefab);
                entityManager.SetComponentData(entity, new Translation { Value = position });
            }
        }
    }

    private void Update()
    {
        GameDataManager.scale = _scale;
        GameDataManager.strength = _strength;
    }
}
