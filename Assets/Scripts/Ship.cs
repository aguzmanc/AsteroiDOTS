using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    HitDetector _asteroidDetector;

    void Start()
    {
        _asteroidDetector.onHitDetected += OnAsteroidHit;
    }

    void OnAsteroidHit(Detectable detectable) 
    {
        GameController.GameOver();
        Destroy(gameObject);
    }
}
