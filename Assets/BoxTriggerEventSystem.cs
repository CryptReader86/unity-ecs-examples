using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class BoxTriggerEventSystem : JobComponentSystem
{
    struct BoxTriggerJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentDataFromEntity<BoxTriggerData> BoxTriggerDataGroup;
        public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup;

        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.Entities.EntityA;
            var entityB = triggerEvent.Entities.EntityB;

            var isTriggerA = BoxTriggerDataGroup.Exists(entityA);
            var isTriggerB = BoxTriggerDataGroup.Exists(entityB);

            if (isTriggerA && isTriggerB) return;

            var isDynamicA = PhysicsVelocityGroup.Exists(entityA);
            var isDynamicB = PhysicsVelocityGroup.Exists(entityB);

            if ((isTriggerA && !isDynamicB) || (isTriggerB && !isDynamicA)) return;

            var triggerEntity = isTriggerA ? entityA : entityB;
            var dynamicEntity = isTriggerA ? entityB : entityA;

            var velocityComponent = PhysicsVelocityGroup[dynamicEntity];
            velocityComponent.Linear = new float3(0, 100, 0);
            PhysicsVelocityGroup[dynamicEntity] = velocityComponent;
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
        var jobHandle = new BoxTriggerJob
        {
            BoxTriggerDataGroup = GetComponentDataFromEntity<BoxTriggerData>(),
            PhysicsVelocityGroup = GetComponentDataFromEntity<PhysicsVelocity>()
        }.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
