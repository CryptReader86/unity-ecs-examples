using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class SheepMoveSystem : JobComponentSystem
{
    private const float ZLimit = 50.0f;

    private const float SheepSpeed = 0.1f;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return Entities
            .WithName("SheepMoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {
                position.Value += SheepSpeed * math.forward(rotation.Value);
                if (position.Value.z >= ZLimit)
                {
                    position.Value.z = -ZLimit;
                }
            })
            .Schedule(inputDeps);
    }
}
