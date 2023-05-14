/*
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class SheepMoveSystem : JobComponentSystem
{
    private static readonly float3 Up = new float3(0.0f, 1.0f, 0.0f);

    private const float YLimit = 50.0f;
    private const float SheepSpeed = 0.1f;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return Entities
            .WithName("SheepMoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {
                position.Value += SheepSpeed * Up;
                if (position.Value.y >= YLimit)
                {
                    position.Value.y = 0;
                }
            })
            .Schedule(inputDeps);
    }
}
*/