using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public class LandscapeSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var scale = GameDataManager.scale;
        var strength = GameDataManager.strength;

        var jobHandle = Entities
            .WithName("LandscapeSystem")
            .ForEach((ref Translation position, ref BlockData blockData) =>
            {
                var perlin = Mathf.PerlinNoise(position.Value.x * scale, position.Value.z * scale) * strength;

                position.Value = new Vector3(position.Value.x, perlin, position.Value.z);
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return inputDeps;
    }
}
