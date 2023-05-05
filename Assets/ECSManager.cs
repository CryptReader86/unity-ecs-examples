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
    }
}
