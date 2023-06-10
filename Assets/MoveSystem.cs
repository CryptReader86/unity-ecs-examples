using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        var waypoints = new NativeArray<float3>(GameDataManager.instance.WaypointsArray, Allocator.TempJob);
        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tankData) =>
               {
                   float3 heading = waypoints[tankData.currentWaipoint] - position.Value;
                   heading.y = 0;
                   quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                   rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * tankData.rotationSpeed);
                   position.Value += deltaTime * tankData.speed * math.forward(rotation.Value);

                   if(math.distance(position.Value, waypoints[tankData.currentWaipoint]) < 1)
                   {
                       tankData.currentWaipoint++;
                       if(tankData.currentWaipoint >= waypoints.Length)
                       {
                           tankData.currentWaipoint = 0;
                       }
                   }
               })
               .Schedule(inputDeps);

        waypoints.Dispose(jobHandle);

        return jobHandle;
    }

}
