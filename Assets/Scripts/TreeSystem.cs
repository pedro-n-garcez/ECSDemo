using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

//component
[GenerateAuthoringComponent]
public struct TreeSpeedData : IComponentData
{
    public float speed;
}

//system
[AlwaysSynchronizeSystem]
public class TreeSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
		float deltaTime = Time.DeltaTime;
		//rotate along Z axis for Tree speed for all entities
		Entities.ForEach((ref Rotation rotation, in TreeSpeedData rotationSpeed) =>
		{
			rotation.Value = math.mul(rotation.Value, quaternion.RotateZ(rotationSpeed.speed * deltaTime));
		}).Run();
		return default;
    }
}