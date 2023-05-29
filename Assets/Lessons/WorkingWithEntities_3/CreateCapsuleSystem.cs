/*
using Unity.Jobs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;

public class CreateCapsuleSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();

        var entity = EntityManager.CreateEntity(
                ComponentType.ReadOnly<LocalToWorld>(),
                ComponentType.ReadOnly<RenderMesh>()
            );

        EntityManager.SetComponentData(entity, new LocalToWorld { Value = float4x4.identity });

        var resourceHolder = Resources.Load<ResourceHolder>("ResourceHolder");

        EntityManager.SetSharedComponentData(entity, new RenderMesh
        {
            mesh = resourceHolder.theMesh,
            material = resourceHolder.theMaterial
        });
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}
*/