using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Physics;
using UnityEngine;

//component
[GenerateAuthoringComponent]
public struct CoconutSpeedData : IComponentData
{
    public float speed;
}

//system
[AlwaysSynchronizeSystem]
public class CoconutMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float2 input = new float2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        //change velocity for coconut speed data for all entities
        Entities.ForEach((ref PhysicsVelocity velocity, in CoconutSpeedData csd) => 
        {
            float2 newvelocity = velocity.Linear.xz;
            newvelocity += input * csd.speed * deltaTime;
            velocity.Linear.xz = newvelocity;
        }).Run();
        return default;
    }
}