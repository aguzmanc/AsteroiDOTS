using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Dragged2DTransform _trX;
    Dragged2DTransform _trY;

    float _lifetime = 9999;


    public void Shoot(float speed, float lifetime) {
        Vector2 dir = transform.up;
        _trX.FixedSpeed(speed * dir.x);
        _trY.FixedSpeed(speed * dir.y);
        _lifetime = lifetime;
    }

    void Awake()
    {
        _trX = gameObject.AddComponent<Dragged2DTransform>();
        _trY = gameObject.AddComponent<Dragged2DTransform>();

        _trX.SetData(Dragged2DTransform.TransformType.TranslationX, 100, 0f, 0f);
        _trY.SetData(Dragged2DTransform.TransformType.TranslationY, 100, 0f, 0f);
    }


    void Update() {
        _lifetime -= Time.deltaTime;
        if(_lifetime < 0f)
            Destroy(gameObject);
    }
}
