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
        float deltaTime = Time.DeltaTime;
        float3 targetLocation = GameDataManager.instance.Player.position;
        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tankData) =>
               {
                   float3 heading = targetLocation - position.Value;
                   heading.y = 0;
                   quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                   rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * tankData.rotationSpeed);
                   position.Value += deltaTime * tankData.speed * math.forward(rotation.Value);
               })
               .Schedule(inputDeps);

        return jobHandle;
    }

}
