using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public enum ObjectType {
        Bullet      = 1,
        Asteroid    = 2,
        Ship        = 3,
        UFO         = 4,
        UFOBullet   = 5
    }


    [SerializeField]
    ObjectType _type;


    public int _objectsDetected;

    public int objectsDetected => _objectsDetected;


    public System.Action<Detectable> onHitDetected;


    void Awake() 
    {
        _objectsDetected = 0;
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        Detectable detectable = other.GetComponent<Detectable>();

        if(detectable) {
            if(other.tag == _type.ToString()) {
                _objectsDetected ++ ;
                if(onHitDetected != null) 
                    onHitDetected(detectable);
            }
        }
    }


    void OnTriggerExit2D(Collider2D other) 
    {
        Detectable detectable = other.GetComponent<Detectable>();

        if(detectable) {
            if(other.tag == _type.ToString()) {
                _objectsDetected -- ;
            }
        }
    }
}
