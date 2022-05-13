using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    Dragged2DTransform _rot;
    Dragged2DTransform _trX;
    Dragged2DTransform _trY;

    void Start() 
    {
        _rot = gameObject.AddComponent<Dragged2DTransform>();
        _rot.SetTransformType(Dragged2DTransform.TransformType.Rotation);

        _trX = gameObject.AddComponent<Dragged2DTransform>();
        _trY = gameObject.AddComponent<Dragged2DTransform>();
    }


    void Update() 
    {
        if(Input.GetKey(KeyCode.RightArrow))
            _rot.Impulse(false);

        if(Input.GetKey(KeyCode.LeftArrow))
            _rot.Impulse(true);

        /*
        if(Input.GetKey(KeyCode.UpArrow))
            _trans.Impulse(transform.up);
            */
    }
}
