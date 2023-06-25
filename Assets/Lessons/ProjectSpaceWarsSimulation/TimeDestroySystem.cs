using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

[UpdateAfter(typeof(MoveBulletSystem))]
public class TimeDestroySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref LifetimeData lifetimeData) =>
            {
                lifetimeData.lifeLeft -= deltaTime;
                if (lifetimeData.lifeLeft <= 0)
                {
                    EntityManager.DestroyEntity(entity);
                }
            })
            .Run();

        return inputDeps;
    }
}
