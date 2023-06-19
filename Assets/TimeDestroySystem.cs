using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

[UpdateAfter(typeof(MoveBulletSystem))]
public class TimeDestroySystem : JobComponentSystem
{
    struct CullingJob : IJobForEachWithEntity<LifetimeData>
    {
        public EntityCommandBuffer.Concurrent commands;
        public float deltaTime;

        public void Execute(Entity entity, int index, ref LifetimeData lifetimeData)
        {
            lifetimeData.lifeLeft -= deltaTime;
            if(lifetimeData.lifeLeft <= 0)
            {
                commands.DestroyEntity(index, entity);
            }
        }
    }

    private EndSimulationEntityCommandBufferSystem _buffer;

    protected override void OnCreate()
    {
        base.OnCreate();

        _buffer = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var cullingJob = new CullingJob
        {
            commands = _buffer.CreateCommandBuffer().ToConcurrent(),
            deltaTime = Time.DeltaTime
        };

        var jobHandle = cullingJob.Schedule(this, inputDeps);
        _buffer.AddJobHandleForProducer(jobHandle);

        jobHandle.Complete();

        return inputDeps;
    }

}
