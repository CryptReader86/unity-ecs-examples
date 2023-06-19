using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class ShootSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var gunLocations = new NativeArray<float3>(GameDataManager.instance.gunLocations, Allocator.TempJob);

        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref Translation position, ref Rotation rotation, ref ShipData shipData) =>
            {
                foreach(var gunLocation in gunLocations)
                {
                    var bullet = EntityManager.Instantiate(shipData.bulletPrefab);
                    EntityManager.SetComponentData(bullet, new Translation { Value = position.Value + math.mul(rotation.Value, gunLocation)});
                    EntityManager.SetComponentData(bullet, new Rotation { Value = rotation.Value });
                    EntityManager.SetComponentData(bullet, new LifetimeData { lifeLeft = 1.0f });
                }
            })
            .Run();

        gunLocations.Dispose();

        return inputDeps;
    }
}
