using UnityEngine;
using TMPro;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSInterface : MonoBehaviour
{
    private World _world;
    private EntityManager _entityManager;

    [SerializeField]
    private TMP_Text _label;

    [SerializeField]
    private GameObject _sheepPrefab;

    [SerializeField]
    private GameObject _palmTreePrefab;

    private void Start()
    {
        _world = World.DefaultGameObjectInjectionWorld;
        _entityManager = _world.GetExistingSystem<MoveSystem>().EntityManager;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_sheepPrefab, new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), 0, UnityEngine.Random.Range(-10.0f, 10.0f)), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            var settings = GameObjectConversionSettings.FromWorld(_world, null);
            var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_palmTreePrefab, settings);

            var palmTree = _entityManager.Instantiate(prefab);
            var palmTreePosition = new float3(UnityEngine.Random.Range(-10.0f, 10.0f), 0, UnityEngine.Random.Range(-10.0f, 10.0f));

            _entityManager.SetComponentData(palmTree, new Translation { Value = palmTreePosition });
            _entityManager.SetComponentData(palmTree, new Rotation { Value = new quaternion(0, 0, 0, 0) });
        }
    }

    public void OnButtonPressed()
    {
        var entityQuery = _entityManager.CreateEntityQuery(ComponentType.ReadOnly<SheepData>());

        _label.text = entityQuery.CalculateEntityCount().ToString();
    }
}
