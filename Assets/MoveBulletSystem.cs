using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;

public class MoveBulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        var jobHandle = Entities
            .WithName("MoveBulletSystem")
            .ForEach((ref PhysicsVelocity physicsVelocity, ref Translation position, ref Rotation rotation, ref BulletData bulletData) =>
            {
                physicsVelocity.Angular = float3.zero;
                physicsVelocity.Linear = deltaTime * bulletData.speed * math.forward(rotation.Value);
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
