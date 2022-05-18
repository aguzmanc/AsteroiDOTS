using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    const int TOTAL_ASTEROIDS_AFTER_EXPLOSSION = 3;

    public enum AsteroidType {
        Big = 1,
        Medium = 2,
        Small = 3
    } 

    Dragged2DTransform rot,tx,ty;

    [Header("Internal Setup")]
    [SerializeField]
    AsteroidType _type = AsteroidType.Big;

    [SerializeField]
    HitDetector _detector;


    public AsteroidType type {get=>_type; set{_type=value;}}


    public void Setup(float rotation, Vector2 impulse) {
        if(rot==null&&tx==null&&ty==null) {
            rot = gameObject.AddComponent<Dragged2DTransform>();
            rot.SetData(Dragged2DTransform.TransformType.Rotation, 0f, 0f, 0f);

            tx = gameObject.AddComponent<Dragged2DTransform>();
            tx.SetData(Dragged2DTransform.TransformType.TranslationX, 0f, 0f, 0f);

            ty = gameObject.AddComponent<Dragged2DTransform>();
            ty.SetData(Dragged2DTransform.TransformType.TranslationY, 0f, 0f, 0f);
        }

        rot.FixedSpeed(rotation);

        Vector2 unit = impulse.normalized;
        float speed = impulse.magnitude;

        tx.FixedSpeed(unit.x * speed);
        ty.FixedSpeed(unit.y * speed);
    }


    void Start() {
        _detector.onHitDetected += OnHitDetected;
    }


    /* Should be hit with bullets */
    void OnHitDetected(Detectable detectable) {
        Destroy(detectable.parentObject);
        ExplodeAsteroid();
    }


    public void ExplodeAsteroid()
    {
        GameController.NotifyAsteroidDestroyed(_type);

        Destroy(gameObject);

        // create secondary asteroids after being hit
        if(_type==AsteroidType.Big)
            AsteroidCreator.CreateMediumAsteroids(TOTAL_ASTEROIDS_AFTER_EXPLOSSION, transform.position);
        else if (_type==AsteroidType.Medium)
            AsteroidCreator.CreateSmallAsteroids(TOTAL_ASTEROIDS_AFTER_EXPLOSSION, transform.position);
        else if (_type==AsteroidType.Small)
            Instantiate(AsteroidCreator.asteroidExplossion, transform.position, Quaternion.identity);
    }
}
