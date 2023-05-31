using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var speed = 5.0f;
        var deltaTime = Time.DeltaTime;
        var targetLocation = new float3(5, 5, 5);

        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tankData) =>
               {
                   position.Value += deltaTime * speed * math.normalize(targetLocation - position.Value);
               })
               .Schedule(inputDeps);

        return jobHandle;
    }

}
