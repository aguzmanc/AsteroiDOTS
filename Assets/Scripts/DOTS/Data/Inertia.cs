using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct InertiaX : IComponentData
{
    public float currentSpeed;
    public float maxSpeed;
    public float impulse;
    public float drag;
}

[Serializable]
public struct InertiaY : IComponentData
{
    public float currentSpeed;
    public float maxSpeed;
    public float impulse;
    public float drag;
}


[Serializable]
public struct InertiaRot : IComponentData
{
    public float currentSpeed;
    public float maxSpeed;
    public float impulse;
    public float drag;
}