using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;

public class AsteroidCreator : MonoBehaviour
{
    [SerializeField]
    GameObject _asteroidPrototype;

    [SerializeField]
    [Range(1,999999)]
    int _totalAsteroids=100;

    [SerializeField]
    [Range(0, 200)]
    float _maxAngleSpeed = 20f;

    [SerializeField]
    [Range(0f, 10f)]
    float _maxImpulse = 4f;

    EntityManager _mgr;
    Entity _asteroidECSPrototype;

    IEnumerator Start() {
        yield return new WaitForSeconds(1);
        for(int i=0;i<_totalAsteroids;i++) {
            Asteroid asteroid = GenerateAsteroid();

            float x = Random.Range(ScreenLimits.leftLimit, ScreenLimits.rightLimit);
            float y = Random.Range(ScreenLimits.downLimit, ScreenLimits.upLimit);

            asteroid.transform.position = new Vector3(x,y,0);
        }
    }


    IEnumerator _Start()
    {
        _mgr =  World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        _asteroidECSPrototype = GameObjectConversionUtility.ConvertGameObjectHierarchy(
                _asteroidPrototype, settings);

        Debug.Log("generation in 3...");
        yield return new WaitForSeconds(3f);

        float initTime = Time.realtimeSinceStartup;
        Debug.Log("init time: " + Time.time);
        for(int i=0;i<_totalAsteroids;i++)
            GenerateAsteroid();

        float diff = (Time.realtimeSinceStartup - initTime);

        Debug.Log("end time: " + Time.realtimeSinceStartup);
        Debug.Log(string.Format("TimeGenerated: {0}", diff));
    }


    Asteroid GenerateAsteroid() 
    {
        Asteroid asteroid = Instantiate(_asteroidPrototype).GetComponent<Asteroid>();

        asteroid.Setup(
            Random.Range(-_maxAngleSpeed, _maxAngleSpeed),
            Random.insideUnitCircle.normalized * Random.Range(0f, _maxImpulse)
        );

        return asteroid;
    }
}
