using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public enum ObjectType {
        Bullet      = 1,
        Asteroid    = 2,
        Ship        = 3,
        UFO         = 4
    }


    [SerializeField]
    ObjectType _type;

    public System.Action<Detectable> onHitDetected;


    void OnTriggerEnter2D(Collider2D other) 
    {
        Detectable detectable = other.GetComponent<Detectable>();

        if(other.GetComponent<Detectable>()) {
            if(other.tag == _type.ToString()) {
                if(onHitDetected != null) 
                    onHitDetected(detectable);
            }
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
