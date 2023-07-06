using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class DuckSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _duckPrefab;
    [SerializeField] private int _numDucks;

    private BlobAssetStore _blobAssetStore;

    private void Start()
    {
        _blobAssetStore = new BlobAssetStore();

        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_duckPrefab, settings);

        for(var i = 0; i < _numDucks; i++)
        {
            var instance = entityManager.Instantiate(prefab);
            var x = UnityEngine.Random.Range(-200.0f, 200.0f);
            var y = UnityEngine.Random.Range(50.0f, 200.0f);
            var z = UnityEngine.Random.Range(-200.0f, 200.0f);
            entityManager.SetComponentData(instance, new Translation { Value = new float3(x, y, z) });
        }
    }

    private void OnDestroy()
    {
        _blobAssetStore.Dispose();
    }
}
