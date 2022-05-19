using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    float _initialY;
    float _angle;

    
    [SerializeField]
    float _speed = 3f;

    [SerializeField]
    float _amplitude = 5f;


    [Header("Internal Setup")]
    [SerializeField]
    HitDetector _bulletDetector;

    [SerializeField]
    GameObject _explossion;


    void Awake()
    {
        _bulletDetector.onHitDetected += OnBulletDetected;
        Dragged2DTransform tx = gameObject.AddComponent<Dragged2DTransform>();
        tx.SetData(Dragged2DTransform.TransformType.TranslationX, 10f, 0f, 0f);
        tx.FixedSpeed(_speed);
        
        _angle = 0f;
        _initialY = transform.position.y;
    }


    void Update() 
    {
        _angle += Time.deltaTime*300;
        float y = _initialY + Mathf.Sin(Mathf.Deg2Rad*_angle)*_amplitude;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, y, 0f);
    }



    void OnBulletDetected(Detectable detectable) 
    {
        _explossion.transform.SetParent(null);
        _explossion.SetActive(true);
        Destroy(gameObject);
    }

}