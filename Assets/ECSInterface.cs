using UnityEngine;
using TMPro;
using Unity.Entities;

public class ECSInterface : MonoBehaviour
{
    private EntityManager _entityManager;

    [SerializeField]
    private TMP_Text _label;

    private void Start()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        _entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;
    }

    public void OnButtonPressed()
    {
        var entityQuery = _entityManager.CreateEntityQuery(ComponentType.ReadOnly<SheepData>());

        _label.text = entityQuery.CalculateEntityCount().ToString();
    }
}
