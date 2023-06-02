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
        var rotationSpeed = 0.5f;
        var deltaTime = Time.DeltaTime;
        var targetLocation = new float3(5, 0, 5);

        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tankData) =>
               {
                   var directionVector = targetLocation - position.Value;
                   var directionQuaternion = quaternion.LookRotation(directionVector, Vector3.up);

                   rotation.Value = math.slerp(rotation.Value, directionQuaternion, deltaTime * rotationSpeed);
                   position.Value += deltaTime * speed * math.normalize(directionVector);
               })
               .Schedule(inputDeps);

        return jobHandle;
    }

}
