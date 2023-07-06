using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _bulletPrefab;

    private BlobAssetStore _blobAssetStore;

    void Start()
    {
        _blobAssetStore = new BlobAssetStore();

        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);

        var player = GameObjectConversionUtility.ConvertGameObjectHierarchy(_playerPrefab, settings);
        var bullet = GameObjectConversionUtility.ConvertGameObjectHierarchy(_bulletPrefab, settings);

        var playerEntity = entityManager.Instantiate(player);
        entityManager.SetComponentData(playerEntity, new Translation { Value = new float3(0, 1.5f, 0) });
        entityManager.SetComponentData(playerEntity, new CharacterData { speed = 100, bulletPrefab = bullet });
    }

    private void OnDestroy()
    {
        _blobAssetStore.Dispose();
    }
}
