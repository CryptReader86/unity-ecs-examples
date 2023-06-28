using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;

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

    [Range(0f, 100f)]
    [SerializeField] private float _sandLevel = 2;
    [Range(0f, 100f)]
    [SerializeField] private float _dirtLevel = 4;
    [Range(0f, 100f)]
    [SerializeField] private float _grassLevel = 6;
    [Range(0f, 100f)]
    [SerializeField] private float _rockLevel = 8;
    [Range(0f, 100f)]
    [SerializeField] private float _snowLevel = 10;

    private const int WorldHalfSize = 75;

    private void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        GameDataManager.sand = GameObjectConversionUtility.ConvertGameObjectHierarchy(_sandPrefab, settings);
        GameDataManager.dirt = GameObjectConversionUtility.ConvertGameObjectHierarchy(_dirtPrefab, settings);
        GameDataManager.grass = GameObjectConversionUtility.ConvertGameObjectHierarchy(_grassPrefab, settings);
        GameDataManager.rock = GameObjectConversionUtility.ConvertGameObjectHierarchy(_rockPrefab, settings);
        GameDataManager.snow = GameObjectConversionUtility.ConvertGameObjectHierarchy(_snowPrefab, settings);

        for (var z = -WorldHalfSize; z <= WorldHalfSize; z++)
        {
            for (var x = -WorldHalfSize; x <= WorldHalfSize; x++)
            {
                var position = new float3(x, 0, z);
                var entity = entityManager.Instantiate(GameDataManager.sand);
                entityManager.SetComponentData(entity, new Translation { Value = position });
                entityManager.SetComponentData(entity, new BlockData { initialPosition = position });
            }
        }
    }

    private void Update()
    {
        if (GameDataManager.scale1 != _scale1) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.scale2 != _scale2) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.scale3 != _scale3) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.strength1 != _strength1) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.strength2 != _strength2) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.strength3 != _strength3) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.sandLevel != _sandLevel) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.dirtLevel != _dirtLevel) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.grassLevel != _grassLevel) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.rockLevel != _rockLevel) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.snowLevel != _snowLevel) GameDataManager.wasDataChanged = true;
        else if (GameDataManager.playerPosition != _player.transform.position) GameDataManager.wasDataChanged = true;

        GameDataManager.playerPosition = _player.transform.position;

        GameDataManager.scale1 = _scale1;
        GameDataManager.scale2 = _scale2;
        GameDataManager.scale3 = _scale3;
        GameDataManager.strength1 = _strength1;
        GameDataManager.strength2 = _strength2;
        GameDataManager.strength3 = _strength3;

        GameDataManager.sandLevel = _sandLevel;
        GameDataManager.dirtLevel = _dirtLevel;
        GameDataManager.grassLevel = _grassLevel;
        GameDataManager.rockLevel = _rockLevel;
        GameDataManager.snowLevel = _snowLevel;
    }
}
