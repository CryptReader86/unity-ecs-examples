using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public class LandscapeSystem : JobComponentSystem
{
    private EntityQuery _blockQuery;

    protected override void OnCreate()
    {
        base.OnCreate();

        _blockQuery = GetEntityQuery(typeof(BlockData));
    }

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

        using (var blocks = _blockQuery.ToEntityArray(Allocator.TempJob))
        {
            foreach (var block in blocks)
            {
                var height = EntityManager.GetComponentData<Translation>(block).Value.y;

                var textureBlock = GameDataManager.dirt;
                if(height <= GameDataManager.sandLevel)
                {
                    textureBlock = GameDataManager.sand;
                }
                else if (height <= GameDataManager.dirtLevel)
                {
                    textureBlock = GameDataManager.dirt;
                }
                else if (height <= GameDataManager.grassLevel)
                {
                    textureBlock = GameDataManager.grass;
                }
                else if (height <= GameDataManager.rockLevel)
                {
                    textureBlock = GameDataManager.rock;
                }
                else
                {
                    textureBlock = GameDataManager.snow;
                }

                var textureBlockRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(textureBlock);
                var blockRenderMesh = EntityManager.GetSharedComponentData<RenderMesh>(block);

                blockRenderMesh = textureBlockRenderMesh;

                EntityManager.SetSharedComponentData(block, blockRenderMesh);
            }
        }

        return inputDeps;
    }
}
