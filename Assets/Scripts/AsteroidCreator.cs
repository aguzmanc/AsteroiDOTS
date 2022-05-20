using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class AsteroidCreator : MonoBehaviour
{
    const int EXCLUSION_RADIUS = 7;

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
    Entity _bigAsteroidECSPrototype;
    Entity _mediumAsteroidECSPrototype;
    Entity _smallAsteroidECSPrototype;

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
            if(GameController.useECS)
                GenerateECSASteroid(_mediumAsteroidECSPrototype, _mediumMaxAngleSpeed, _mediumMaxImpulse,
                position.x, position.y, Asteroid.AsteroidType.Medium);
            else
                GenerateAsteroid(_mediumAsteroidPrototype, _mediumMaxAngleSpeed, _mediumMaxImpulse,
                position.x, position.y, Asteroid.AsteroidType.Medium);
        }
    }


    void _CreateSmallAsteroids(int total, Vector2 position) {
        for(int i=0;i<total;i++) {
            if(GameController.useECS)
                GenerateECSASteroid(_smallAsteroidECSPrototype, _bigMaxAngleSpeed, _bigMaxImpulse,
                    position.x, position.y, Asteroid.AsteroidType.Small);
            else
                GenerateAsteroid(_smallAsteroidPrototype, _smallMaxAngleSpeed, _smallMaxImpulse,
                    position.x, position.y, Asteroid.AsteroidType.Small);
        }
    }


    void Awake() {
        _instance = this;
        GameController.onGameStarted += OnGameStarted;
        GameController.onAllAsteroidsDestroyed += OnAllAsteroidsDestroyed;
    }

    void Start(){
        if(GameController.useECS) {
            _mgr =  World.DefaultGameObjectInjectionWorld.EntityManager;
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            _bigAsteroidECSPrototype = GameObjectConversionUtility.ConvertGameObjectHierarchy(_bigAsteroidPrototype, settings);
            _mediumAsteroidECSPrototype = GameObjectConversionUtility.ConvertGameObjectHierarchy(_mediumAsteroidPrototype, settings);
            _smallAsteroidECSPrototype = GameObjectConversionUtility.ConvertGameObjectHierarchy(_smallAsteroidPrototype, settings);
        }
    }


    void OnDestroy() {
        GameController.onGameStarted -= OnGameStarted;
        GameController.onAllAsteroidsDestroyed -= OnAllAsteroidsDestroyed;
    }



    void OnGameStarted(int ships) {
        GenerateAsteroidCloud();
    }

    void OnAllAsteroidsDestroyed()
    {
        Invoke("GenerateAsteroidCloud", 1f);
    }


    void GenerateAsteroidCloud() {
        /* Create big asteroids */
        for(int i=0;i<_asteroidsAtBeginning;i++) {
            float x=0,y=0;
            Vector2 pos;
            do{
                x = UnityEngine.Random.Range(ScreenLimits.leftLimit, ScreenLimits.rightLimit);
                y = UnityEngine.Random.Range(ScreenLimits.downLimit, ScreenLimits.upLimit);
                pos = new Vector2(x,y);
            } while(pos.magnitude < EXCLUSION_RADIUS);

            if(GameController.useECS)
                GenerateECSASteroid(_bigAsteroidECSPrototype, _bigMaxAngleSpeed, _bigMaxImpulse,
                    x,y, Asteroid.AsteroidType.Big);
            else
                GenerateAsteroid(_bigAsteroidPrototype, _bigMaxAngleSpeed, _bigMaxImpulse,
                    x,y, Asteroid.AsteroidType.Big);
        }
    }



    void GenerateECSASteroid(Entity entityPrototype, float maxAngleSpeed, float maxImpulse,
        float x, float y, Asteroid.AsteroidType asteroidType)
    {
        
        Entity asteroid = _mgr.Instantiate(entityPrototype);
        _mgr.SetComponentData(entityPrototype, new Translation(){Value=new float3(x,y,0)});
        _mgr.SetComponentData(entityPrototype, new Rotation(){Value=transform.rotation});


        Vector2 velocity = UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0f, maxImpulse);
        float rotationSpeed = UnityEngine.Random.Range(-maxAngleSpeed, maxAngleSpeed);


        _mgr.AddComponentData(asteroid, new InertiaX(){
            currentSpeed = velocity.x
        });

        _mgr.AddComponentData(asteroid, new InertiaY() {
            currentSpeed = velocity.y
        });

        _mgr.AddComponentData(asteroid, new InertiaRot() {
            currentSpeed = rotationSpeed
        });

        GameController.NotifyAsteroidCreated();
    }


    void GenerateAsteroid(GameObject prototype, float maxAngleSpeed, float maxImpulse,
        float x, float y, Asteroid.AsteroidType asteroidType) 
    {
        Asteroid asteroid = Instantiate(prototype).GetComponent<Asteroid>();

        asteroid.Setup(
            UnityEngine.Random.Range(-maxAngleSpeed, maxAngleSpeed),
            UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0f, maxImpulse)
        );

        asteroid.type = asteroidType;
        asteroid.transform.position = new Vector2(x,y);

        GameController.NotifyAsteroidCreated();
    }
}
