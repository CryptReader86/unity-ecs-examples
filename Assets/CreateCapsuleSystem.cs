using Unity.Jobs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;

public class CreateCapsuleSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();

        var entity = EntityManager.CreateEntity(
                ComponentType.ReadWrite<Translation>(),
                ComponentType.ReadWrite<Rotation>(),
                ComponentType.ReadOnly<RenderMesh>()
            );

        EntityManager.SetComponentData(entity, new Translation { Value = float3.zero });
        EntityManager.SetComponentData(entity, new Rotation { Value = quaternion.identity });
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}
