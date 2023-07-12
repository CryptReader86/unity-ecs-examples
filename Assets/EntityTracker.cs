using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class EntityTracker : MonoBehaviour
{
    private Entity _entityToTrack = Entity.Null;

    public Entity EntityToTrack { set { _entityToTrack = value; } }

    private void LateUpdate()
    {
        if (_entityToTrack != Entity.Null)
        {
            try
            {
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                transform.position = entityManager.GetComponentData<Translation>(_entityToTrack).Value;
                transform.rotation = entityManager.GetComponentData<Rotation>(_entityToTrack).Value;
            }
            catch
            {
                _entityToTrack = Entity.Null;
            }
        }
    }
}
