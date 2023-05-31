/*
using Unity.Jobs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;

public class CreateSheepSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();

        var resourceHolder = Resources.Load<ResourceHolder>("ResourceHolder");

        for(var i = 1; i <= 100; i++)
        {
            var entity = EntityManager.CreateEntity(
                ComponentType.ReadOnly<LocalToWorld>(),
                ComponentType.ReadOnly<RenderMesh>(),
                ComponentType.ReadOnly<Translation>(),
                ComponentType.ReadOnly<Rotation>(),
                ComponentType.ReadOnly<NonUniformScale>()
            );

            var position = new float3(UnityEngine.Random.Range(-50.0f, 50.0f), 0, UnityEngine.Random.Range(-50.0f, 50.0f));

            EntityManager.SetComponentData(entity, new LocalToWorld { Value = float4x4.identity });
            EntityManager.SetComponentData(entity, new Translation { Value = position });
            EntityManager.SetComponentData(entity, new Rotation { Value = quaternion.identity });
            EntityManager.SetComponentData(entity, new NonUniformScale { Value = new float3(UnityEngine.Random.Range(50.0f, 100.0f)) });

            EntityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = resourceHolder.theMesh,
                material = resourceHolder.theMaterial
            });
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}
*/