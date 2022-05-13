using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragged2DTransform : MonoBehaviour
{
    public enum TransformType{
        Rotation = 1,
        TranslationX = 2,
        TranslationY = 3
    }

    [SerializeField]
    TransformType _transformType = TransformType.Rotation;

    [SerializeField]
    [Range(0f, 200f)]
    float _maxSpeed = 40; // Unit per second forward

    [SerializeField]
    [Range(0f, 100f)]
    float _gain = 3; // how much speed gains every second  (with intervention)

    [SerializeField]
    [Range(0f, 100f)]
    float _drag = 3; // How much speed looses every second (without intervention)


    [Header("-- DEBUG --")]
    public float _currentSpeed;


    public void SetTransformType(TransformType trType) {
        _transformType = trType;
    }


    /* Changes speed */
    public void Impulse(bool reverse) 
    {
        _currentSpeed = _currentSpeed + (reverse ? -1f:1f) * _gain * Time.deltaTime;

        _currentSpeed = Mathf.Clamp(_currentSpeed, -_maxSpeed, _maxSpeed);
    }



    void Update()
    {
        switch(_transformType){
            case TransformType.Rotation:
                transform.Rotate(0, 0, _currentSpeed * Time.deltaTime);
                break;

            case TransformType.TranslationX:
                transform.Translate(_currentSpeed * Time.deltaTime, 0f, 0f);
                break;

            case TransformType.TranslationY:
                transform.Translate(0f, _currentSpeed * Time.deltaTime, 0f);
                break;
        }

        bool positive = _currentSpeed > 0;

        if(positive) {
            _currentSpeed = _currentSpeed - (Time.deltaTime * _drag);
            _currentSpeed = Mathf.Max(0f, _currentSpeed);
        } else {
            _currentSpeed = _currentSpeed + (Time.deltaTime * _drag);
            _currentSpeed = Mathf.Min(0f, _currentSpeed);
        }
    }
}
