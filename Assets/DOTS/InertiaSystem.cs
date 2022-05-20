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
        // Assign values to local variables captured in your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        //     float deltaTime = Time.DeltaTime;

        // This declares a new kind of job, which is a unit of work to do.
        // The job is declared as an Entities.ForEach with the target components as parameters,
        // meaning it will process all entities in the world that have both
        // Translation and Rotation components. Change it to process the component
        // types you want.
        
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

            /*
            if(_impulseFactor != 0f) {
                _currentSpeed = _currentSpeed + _impulseFactor * _impulse * Time.deltaTime;
                _currentSpeed = Mathf.Clamp(_currentSpeed, -_maxSpeed, _maxSpeed);
                _impulseFactor = 0f;
            } else { // when not impulsing, is dragging
                bool positive = _currentSpeed >= 0;
                if(positive) {
                    _currentSpeed = _currentSpeed - (Time.deltaTime * _drag);
                    _currentSpeed = Mathf.Max(0f, _currentSpeed);
                } else {
                    _currentSpeed = _currentSpeed + (Time.deltaTime * _drag);
                    _currentSpeed = Mathf.Min(0f, _currentSpeed);
                }
            }
            */
    }
}
