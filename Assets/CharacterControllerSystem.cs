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

        return inputDeps;
    }
}
