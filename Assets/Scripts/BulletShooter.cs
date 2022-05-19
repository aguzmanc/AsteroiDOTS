using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void GenerateBullet() 
    {
        GameObject obj = (GameObject)Instantiate(_bulletPrototype, transform.position, transform.rotation);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.Shoot(_speed, _lifetime);
    }
}
