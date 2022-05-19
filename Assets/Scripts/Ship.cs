using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    HitDetector _asteroidDetector;

    [SerializeField]
    HitDetector _UFOBulletDetector;

    [SerializeField]
    GameObject _shipExplossion;


    bool _withImpulse;

    public bool withImpulse{get=>_withImpulse; set{_withImpulse=value;}}


    void Start()
    {
        _asteroidDetector.onHitDetected += OnAsteroidHit;
        _UFOBulletDetector.onHitDetected += OnUFOBulletHit;
        GameController.RegisterShip(this);
    }


    void OnDestroy() {
        GameController.RegisterShip(null);

        _asteroidDetector.onHitDetected -= OnAsteroidHit;
        _UFOBulletDetector.onHitDetected -= OnUFOBulletHit;
    }


    void OnAsteroidHit(Detectable detectable) 
    {
        Asteroid asteroid = detectable.parentObject.GetComponent<Asteroid>();
        asteroid.ExplodeAsteroid();

        DestroyShip();
    }
    

    void OnUFOBulletHit(Detectable detectable) {
        Destroy(detectable.parentObject);
        DestroyShip();
    }


    void DestroyShip(){
        Instantiate(_shipExplossion, transform.position, Quaternion.identity);
        GameController.NotifyShipDestroyed();
        Destroy(gameObject);
    }
}
