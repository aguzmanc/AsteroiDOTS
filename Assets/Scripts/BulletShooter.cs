using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class BulletShooter : MonoBehaviour
{
    [SerializeField]
    GameObject _bulletPrototype;

    [SerializeField]
    [Range(1f, 30f)]
    float _speed = 10f;

    [SerializeField]
    [Range(1f, 10f)]
    float _lifetime = 3f;


    // ECS
    EntityManager _mgr;
    Entity _bulletECSPrototype;


    void Start(){
        if(GameController.useECS) {
            _mgr =  World.DefaultGameObjectInjectionWorld.EntityManager;
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            _bulletECSPrototype = GameObjectConversionUtility.ConvertGameObjectHierarchy(
                _bulletPrototype, settings);

        }
    }


    public void GenerateBullet() 
    {
        if(GameController.useECS) {
            Entity bullet = _mgr.Instantiate(_bulletECSPrototype);
            _mgr.SetComponentData(bullet, new Translation(){Value=transform.position});
            _mgr.SetComponentData(bullet, new Rotation(){Value=transform.rotation});
            

            _mgr.AddComponentData(bullet, new InertiaX(){
                currentSpeed = transform.up.x * _speed
            });

            _mgr.AddComponentData(bullet, new InertiaY() {
                currentSpeed = transform.up.y * _speed
            });

            TimeToLive timeToLive = new TimeToLive { Value = _lifetime };
            _mgr.AddComponentData(bullet, timeToLive);

        } else {
            GameObject obj = (GameObject)Instantiate(_bulletPrototype, transform.position, transform.rotation);
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.Shoot(_speed, _lifetime);
        }
    }
}
