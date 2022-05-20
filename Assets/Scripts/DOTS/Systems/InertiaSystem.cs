using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class InertiaSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        
        Entities.ForEach((ref Translation pos, in InertiaX inertia) 
        => {
            pos.Value = pos.Value + (dt * inertia.currentSpeed * math.right());
        }).Schedule();



        Entities.ForEach((ref Translation pos, in InertiaY inertia) 
        => {
            pos.Value = pos.Value + (dt * inertia.currentSpeed * math.up());
        }).Schedule();


        Entities.ForEach((ref Rotation rotation, in InertiaRot inertia) 
        => {
            float rad = math.radians(dt * inertia.currentSpeed);

            rotation.Value = math.mul(rotation.Value.value, quaternion.Euler(new float3(){z=rad}));
        }).Schedule();
    }
}
