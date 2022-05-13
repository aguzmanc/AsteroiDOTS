using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragged2DTranslation : MonoBehaviour
{
    [SerializeField]
    float _speed = 40; // Unit per second forward

    [SerializeField]
    float _drag = 3; // How much speed looses every second


    [Header("-- DEBUG --")]
    public Vector2 _direction = Vector2.up;
    public float _currentSpeed;
    public bool _reverse;


    /* Makes CurrAngSpeed be the same as initial AngularSpeed*/
    public void ResetSpeed(bool reverse=false) 
    {
        _reverse = reverse;
        _currentSpeed = _speed;
    }



    void Update()
    {
        transform.Translate(0f, (_reverse?-1f:1f) * _currentSpeed * Time.deltaTime, 0f);

        _currentSpeed = _currentSpeed - (Time.deltaTime * _drag);
        _currentSpeed = Mathf.Max(0f, _currentSpeed);
    }
}
