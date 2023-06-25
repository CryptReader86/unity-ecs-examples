using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class MoveBulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        var jobHandle = Entities
               .WithName("MoveBulletSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref BulletData bulletData) =>
               {
                   position.Value += deltaTime * 100 * math.forward(rotation.Value);
               })
               .Schedule(inputDeps);

        jobHandle.Complete();

        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref Translation position, ref Rotation rotation, ref BulletData bulletData, ref LifetimeData lifetime) =>
            {
                var distanceToPlanet = math.length(GameDataManager.instance.wps[bulletData.waypoint] - position.Value);
                if(distanceToPlanet < 25)
                {
                    lifetime.lifeLeft = 0;

                    var explosion = EntityManager.Instantiate(bulletData.explosionPrefab);
                    EntityManager.SetComponentData(explosion, new Translation { Value = position.Value });
                    EntityManager.SetComponentData(explosion, new Rotation { Value = rotation.Value });
                    EntityManager.SetComponentData(explosion, new LifetimeData { lifeLeft = 1.0f });
                }
            })
            .Run();


        return jobHandle;
    }
}
