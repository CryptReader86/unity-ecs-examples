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
        var scale1 = GameDataManager.scale1;
        var scale2 = GameDataManager.scale2;
        var scale3 = GameDataManager.scale3;
        var strength1 = GameDataManager.strength1;
        var strength2 = GameDataManager.strength2;
        var strength3 = GameDataManager.strength3;

        var jobHandle = Entities
            .WithName("LandscapeSystem")
            .ForEach((ref Translation position, ref BlockData blockData) =>
            {
                var perlin1 = Mathf.PerlinNoise(position.Value.x * scale1, position.Value.z * scale1) * strength1;
                var perlin2 = Mathf.PerlinNoise(position.Value.x * scale2, position.Value.z * scale2) * strength2;
                var perlin3 = Mathf.PerlinNoise(position.Value.x * scale3, position.Value.z * scale3) * strength3;

                var height = perlin1 + perlin2 + perlin3;

                position.Value = new Vector3(position.Value.x, height, position.Value.z);
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return inputDeps;
    }
}
