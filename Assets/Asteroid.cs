using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Dragged2DTransform rot,tx,ty;

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
}
