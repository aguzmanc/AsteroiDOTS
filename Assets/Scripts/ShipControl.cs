using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [Header("-- ROTATION --")]
    [SerializeField]
    [Range(30,200)]
    float _rotationSpeed = 120;

    [SerializeField]
    [Range(30,200)]
    float _rotationAcceleration = 100;

    [Header("-- ROTATION --")]
    [SerializeField]
    [Range(30,200)]
    float _movementSpeed = 10;

    [SerializeField]
    [Range(30,200)]
    float _movementAcceleration = 10;


    Dragged2DTransform _rot;
    Dragged2DTransform _trX;
    Dragged2DTransform _trY;

    void Start() 
    {
        _rot = gameObject.AddComponent<Dragged2DTransform>();
        _rot.SetData(
            Dragged2DTransform.TransformType.Rotation,
            _rotationSpeed, _rotationAcceleration, _rotationAcceleration*2
        );

        _trX = gameObject.AddComponent<Dragged2DTransform>();
        _trX.SetData(
            Dragged2DTransform.TransformType.TranslationX,
            _movementSpeed, _movementAcceleration, 0
        );

        _trY = gameObject.AddComponent<Dragged2DTransform>();
        _trY.SetData(
            Dragged2DTransform.TransformType.TranslationY,
            _movementSpeed, _movementAcceleration, 0
        );
    }


    void Update() 
    {
        if(Input.GetKey(KeyCode.RightArrow))
            _rot.Impulse(-1f);

        if(Input.GetKey(KeyCode.LeftArrow))
            _rot.Impulse(1f);


        if(Input.GetKey(KeyCode.UpArrow)){
            _trX.Impulse(transform.up.x);
            _trY.Impulse(transform.up.y);
        }


        if(Input.GetKey(KeyCode.DownArrow)){
            _trX.Impulse(-transform.up.x);
            _trY.Impulse(-transform.up.y);
        }
    }
}
