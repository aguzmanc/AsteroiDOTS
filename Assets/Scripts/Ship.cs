using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    HitDetector _asteroidDetector;

    [SerializeField]
    GameObject _shipExplossion;


    void Start()
    {
        _asteroidDetector.onHitDetected += OnAsteroidHit;
    }


    void OnAsteroidHit(Detectable detectable) 
    {
        Asteroid asteroid = detectable.parentObject.GetComponent<Asteroid>();
        asteroid.ExplodeAsteroid();

        Instantiate(_shipExplossion, transform.position, Quaternion.identity);

        GameController.GameOver();
        Destroy(gameObject);
    }
}
