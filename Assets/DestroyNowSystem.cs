using Unity.Entities;
using Unity.Jobs;

[UpdateAfter(typeof(MoveBulletSystem))]
public class DestroyNowSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref DestroyNowData destroyNowData) =>
            {
                if (destroyNowData.shouldDestroy)
                {
                    EntityManager.DestroyEntity(entity);
                }
            })
            .Run();

        return inputDeps;
    }
}
