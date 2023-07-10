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
        public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup;

        public void Execute(CollisionEvent collisionEvent)
        {
            var entityA = collisionEvent.Entities.EntityA;
            var entityB = collisionEvent.Entities.EntityB;

            var isTargetA = PhysicsVelocityGroup.Exists(entityA);
            var isTargetB = PhysicsVelocityGroup.Exists(entityB);

            var isBulletA = BulletGroup.Exists(entityA);
            var isBulletB = BulletGroup.Exists(entityB);

            if(isTargetA && isBulletB)
            {
                var targetVelocity = PhysicsVelocityGroup[entityA];
                targetVelocity.Linear = BulletGroup[entityB].collisionEffect;
                PhysicsVelocityGroup[entityA] = targetVelocity;
            }
            else if (isTargetB && isBulletA)
            {
                var targetVelocity = PhysicsVelocityGroup[entityB];
                targetVelocity.Linear = BulletGroup[entityA].collisionEffect;
                PhysicsVelocityGroup[entityB] = targetVelocity;
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
            PhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>()
        }.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
