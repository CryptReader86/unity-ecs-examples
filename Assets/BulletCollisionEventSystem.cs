using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class BulletCollisionEventSystem : JobComponentSystem
{
    private struct CollisionEventJob : ICollisionEventsJob
    {
        [ReadOnly]
        public ComponentDataFromEntity<BulletData> BulletGroup;
        public ComponentDataFromEntity<DestroyNowData> DestroyNowGroup;

        public void Execute(CollisionEvent collisionEvent)
        {
            var entityA = collisionEvent.Entities.EntityA;
            var entityB = collisionEvent.Entities.EntityB;

            var isTargetA = DestroyNowGroup.Exists(entityA);
            var isTargetB = DestroyNowGroup.Exists(entityB);

            var isBulletA = BulletGroup.Exists(entityA);
            var isBulletB = BulletGroup.Exists(entityB);

            if(isTargetA && isBulletB)
            {
                var destroyNowData = DestroyNowGroup[entityA];
                destroyNowData.shouldDestroy = true;
                DestroyNowGroup[entityA] = destroyNowData;
            }
            else if (isTargetB && isBulletA)
            {
                var destroyNowData = DestroyNowGroup[entityB];
                destroyNowData.shouldDestroy = true;
                DestroyNowGroup[entityB] = destroyNowData;
            }
        }
    }

    private BuildPhysicsWorld _buildPhysicsWorld;
    private StepPhysicsWorld _stepPhysicsWorld;

    protected override void OnCreate()
    {
        _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = new CollisionEventJob
        {
            BulletGroup = GetComponentDataFromEntity<BulletData>(),
            DestroyNowGroup = GetComponentDataFromEntity<DestroyNowData>()
        }.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
