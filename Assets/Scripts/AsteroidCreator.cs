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


    EntityManager _mgr;
    Entity _asteroidECSPrototype;

    IEnumerator Start() {
        yield return new WaitForSeconds(1);
        for(int i=0;i<_totalAsteroids;i++) {
            Asteroid asteroid = Instantiate(_asteroidPrototype).GetComponent<Asteroid>();
            asteroid.Setup(40f, new Vector2(1,4));
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


    void GenerateAsteroid() 
    {
        Vector3 pos = Random.onUnitSphere * Random.Range(1f, 5f);
        Instantiate(_asteroidPrototype, pos, Quaternion.identity);
    }
}
