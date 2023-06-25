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
        var waypoints = new NativeArray<float3>(GameDataManager.instance.wps, Allocator.TempJob);
        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref ShipData shipData) =>
               {
                   var distance = math.distance(position.Value, waypoints[shipData.currentWP]);
                   if(distance < 60)
                   {
                       shipData.approach = false;
                   }
                   else if (distance > 300)
                   {
                       shipData.approach = true;
                   }

                   var heading = (shipData.approach ? 1 : -1) * waypoints[shipData.currentWP] - position.Value;

                   quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                   rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * shipData.rotationSpeed);
                   position.Value += deltaTime * shipData.speed * math.forward(rotation.Value);
               })
               .Schedule(inputDeps);

        waypoints.Dispose(jobHandle);

        return jobHandle;
    }

}
