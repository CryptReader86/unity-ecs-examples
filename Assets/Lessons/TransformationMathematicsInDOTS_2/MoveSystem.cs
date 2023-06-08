/*
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
        var targetLocation = float3.zero;

        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position) =>
               {
                   var pivot = targetLocation;
                   var rotationSpeed = speed * deltaTime * (1 / math.distance(position.Value, pivot));
                   position.Value = math.mul(quaternion.AxisAngle(Vector3.up, rotationSpeed), position.Value - pivot) + pivot;
               })
               .Schedule(inputDeps);

        return jobHandle;
    }

}
*/