using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    private EntityManager _entityManager;

    [SerializeField]
    private int _numSheep;

    [SerializeField]
    private GameObject _sheepPrefab;

    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_sheepPrefab, settings);

        for (var i = 0; i < _numSheep; i++)
        {
            var sheep = _entityManager.Instantiate(prefab);
            var sheepPosition = new float3(UnityEngine.Random.Range(-50.0f, 50.0f), 0, UnityEngine.Random.Range(-50.0f, 50.0f));

            _entityManager.SetComponentData(sheep, new Translation { Value = sheepPosition });
            _entityManager.SetComponentData(sheep, new Rotation { Value = new quaternion(0, 0, 0, 0) });
        }
    }
}
