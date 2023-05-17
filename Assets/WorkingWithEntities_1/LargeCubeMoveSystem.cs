/*
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class LargeCubeMoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref LargeCubeData largeCubeData) =>
            {
                position.Value -= 0.01f * math.up();
            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}
*/