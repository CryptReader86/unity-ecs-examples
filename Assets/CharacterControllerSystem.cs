using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class CharacterControllerSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var deltaTime = Time.DeltaTime;
        var inputY = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");
        var shooting = Input.GetAxis("Fire1");

        var jobHandle = Entities
            .WithName("CharacterControllerSystem")
            .ForEach((ref PhysicsVelocity physicsVelocity, ref Rotation rotation, ref CharacterData characterData) =>
            {
                if (inputZ == 0)
                    physicsVelocity.Linear = float3.zero;
                else
                    physicsVelocity.Linear = inputZ * deltaTime * characterData.speed * math.forward(rotation.Value);

                rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), deltaTime * inputY));
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        Entities.WithoutBurst().WithStructuralChanges()
            .WithName("ShootControllerSystem")
            .ForEach((ref PhysicsVelocity physicsVelocity, ref Translation position, ref Rotation rotation, ref CharacterData characterData) =>
            {
                if(shooting > 0)
                {
                    var bullet = EntityManager.Instantiate(characterData.bulletPrefab);

                    var bulletPositionOffset = new float3(UnityEngine.Random.Range(-1, 2), 1, 1);

                    EntityManager.SetComponentData(bullet, new Translation { Value = position.Value + math.mul(rotation.Value, bulletPositionOffset) });
                    EntityManager.SetComponentData(bullet, new Rotation { Value = rotation.Value });

                    var speed = UnityEngine.Random.Range(20, 150);
                    var collisionEffect = new float3(UnityEngine.Random.Range(-500, 500), UnityEngine.Random.Range(-500, 500), UnityEngine.Random.Range(-500, 500));

                    EntityManager.SetComponentData(bullet, new BulletData { speed = speed, collisionEffect = collisionEffect });
                }
            })
            .Run();

        return inputDeps;
    }
}
