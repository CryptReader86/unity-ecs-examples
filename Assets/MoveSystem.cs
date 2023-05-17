﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;


public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {
            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}
