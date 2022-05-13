using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DraggedRotation))]
[RequireComponent(typeof(Dragged2DTranslation))]
public class ShipControl : MonoBehaviour
{
    DraggedRotation _rot;
    Dragged2DTranslation _trans;

    void Start() 
    {
        _rot = GetComponent<DraggedRotation>();
        _trans = GetComponent<Dragged2DTranslation>();
    }


    void Update() 
    {
        if(Input.GetKey(KeyCode.RightArrow)) {
            _rot.ResetAngularSpeed();
        }

        if(Input.GetKey(KeyCode.LeftArrow)) {
            _rot.ResetAngularSpeed(true);
        }

        if(Input.GetKey(KeyCode.UpArrow))
            _trans.ResetSpeed();
    }
}
