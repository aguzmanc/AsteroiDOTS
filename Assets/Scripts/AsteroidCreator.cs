using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;

public class AsteroidCreator : MonoBehaviour
{
    static AsteroidCreator _instance;


    [SerializeField]
    [Range(1,999999)]
    int _asteroidsAtBeginning=100;

    [SerializeField]
    GameObject _asteroidExplossion;

    [Header("Big Asteroids")]
    [SerializeField]
    GameObject _bigAsteroidPrototype;

    [SerializeField]
    [Range(0, 200)]
    float _bigMaxAngleSpeed = 20f;

    [SerializeField]
    [Range(0f, 10f)]
    float _bigMaxImpulse = 4f;


    [Header("Medium Asteroids")]
    [SerializeField]
    GameObject _mediumAsteroidPrototype;

    [SerializeField]
    [Range(0, 200)]
    float _mediumMaxAngleSpeed = 20f;

    [SerializeField]
    [Range(0f, 10f)]
    float _mediumMaxImpulse = 4f;


    [Header("Small Asteroids")]
    [SerializeField]
    GameObject _smallAsteroidPrototype;

    [SerializeField]
    [Range(0, 200)]
    float _smallMaxAngleSpeed = 20f;

    [SerializeField]
    [Range(0f, 10f)]
    float _smallMaxImpulse = 4f;


    EntityManager _mgr;
    Entity _asteroidECSPrototype;

    public static GameObject asteroidExplossion => _instance._asteroidExplossion;


    public static void CreateMediumAsteroids(int total, Vector2 position) 
    {
        _instance._CreateMediumAsteroids(total, position);
    }

    public static void CreateSmallAsteroids(int total, Vector2 position) 
    {
        _instance._CreateSmallAsteroids(total, position);
    }


    /* When a big asteroid explodes*/
    void _CreateMediumAsteroids(int total, Vector2 position) {
        for(int i=0;i<total;i++) {
            Asteroid asteroid = GenerateAsteroid(_mediumAsteroidPrototype, _mediumMaxAngleSpeed, _mediumMaxImpulse);
            asteroid.transform.position = new Vector3(position.x, position.y,0);
            asteroid.type = Asteroid.AsteroidType.Medium;
        }
    }

    void _CreateSmallAsteroids(int total, Vector2 position) {
        for(int i=0;i<total;i++) {
            Asteroid asteroid = GenerateAsteroid(_smallAsteroidPrototype, _smallMaxAngleSpeed, _smallMaxImpulse);
            asteroid.transform.position = new Vector3(position.x, position.y,0);
            asteroid.type = Asteroid.AsteroidType.Small;
        }
    }


    void Awake() {
        _instance = this;
        GameController.onGameStarted += OnGameStarted;
    }


    IEnumerator _Start()
    {
        _mgr =  World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        _asteroidECSPrototype = GameObjectConversionUtility.ConvertGameObjectHierarchy(
                _bigAsteroidPrototype, settings);

        Debug.Log("generation in 3...");
        yield return new WaitForSeconds(3f);

        float initTime = Time.realtimeSinceStartup;
        Debug.Log("init time: " + Time.time);
        /*for(int i=0;i<_asteroidsAtBeginning;i++)
            GenerateAsteroid();
            */

        float diff = (Time.realtimeSinceStartup - initTime);

        Debug.Log("end time: " + Time.realtimeSinceStartup);
        Debug.Log(string.Format("TimeGenerated: {0}", diff));
    }


    void OnGameStarted() {
        /* Create big asteroids */
        for(int i=0;i<_asteroidsAtBeginning;i++) {
            Asteroid asteroid = GenerateAsteroid(_bigAsteroidPrototype, _bigMaxAngleSpeed, _bigMaxImpulse);

            float x = Random.Range(ScreenLimits.leftLimit, ScreenLimits.rightLimit);
            float y = Random.Range(ScreenLimits.downLimit, ScreenLimits.upLimit);

            asteroid.transform.position = new Vector3(x,y,0);
            asteroid.type = Asteroid.AsteroidType.Big;
        }
    }


    Asteroid GenerateAsteroid(GameObject prototype, float maxAngleSpeed, float maxImpulse) 
    {
        Asteroid asteroid = Instantiate(prototype).GetComponent<Asteroid>();

        asteroid.Setup(
            Random.Range(-maxAngleSpeed, maxAngleSpeed),
            Random.insideUnitCircle.normalized * Random.Range(0f, maxImpulse)
        );

        return asteroid;
    }
}
