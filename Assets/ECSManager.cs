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
    [SerializeField] private float _strength1 = 1.0f;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _strength2 = 1.0f;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _strength3 = 1.0f;
    [Range(0.01f, 1.0f)]
    [SerializeField] private float _scale1 = 0.1f;
    [Range(0.01f, 1.0f)]
    [SerializeField] private float _scale2 = 0.1f;
    [Range(0.01f, 1.0f)]
    [SerializeField] private float _scale3 = 0.1f;

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
        GameDataManager.scale1 = _scale1;
        GameDataManager.scale2 = _scale2;
        GameDataManager.scale3 = _scale3;
        GameDataManager.strength1 = _strength1;
        GameDataManager.strength2 = _strength2;
        GameDataManager.strength3 = _strength3;
    }
}
