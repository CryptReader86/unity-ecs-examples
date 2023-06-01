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
        var speed = 1.0f;
        var deltaTime = Time.DeltaTime;
        var targetLocation = new float3(5, 5, 5);

        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tankData) =>
               {
                   var direction = math.normalize(targetLocation - position.Value);

                   rotation.Value = quaternion.LookRotation(direction, Vector3.up);
                   position.Value += deltaTime * speed * direction;
               })
               .Schedule(inputDeps);

        return jobHandle;
    }

}
