using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedRotation : MonoBehaviour
{
    [SerializeField]
    float _angularSpeed = 40; // Angles per second

    [SerializeField]
    float _angularDrag = 3; // How much speed looses every second


    [Header("-- DEBUG --")]
    public float _currentAngularSpeed;
    public bool _reverse;


    /* Makes CurrAngSpeed be the same as initial AngularSpeed*/
    public void ResetAngularSpeed(bool reverse=false) 
    {
        _reverse = reverse;
        _currentAngularSpeed = _angularSpeed;
    }



    void Update()
    {
        transform.Rotate(0, 0, (_reverse?-1f:1f) * _currentAngularSpeed * Time.deltaTime);

        _currentAngularSpeed = _currentAngularSpeed - (Time.deltaTime * _angularDrag);
        _currentAngularSpeed = Mathf.Max(0f, _currentAngularSpeed);
    }
}
