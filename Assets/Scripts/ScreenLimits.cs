using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenLimits : MonoBehaviour
{
    static ScreenLimits _instance;

    [SerializeField]
    [Range(-5f, 5f)]
    float _margin = 2f;

    static float _verticalSize, _horizontalSize;



    public static float upLimit => (_verticalSize+_instance._margin);

    public static float downLimit => (-_verticalSize-_instance._margin);

    public static float rightLimit => (_horizontalSize+_instance._margin);

    public static float leftLimit => (-_horizontalSize-_instance._margin);



    void Awake() {
        _instance = this;
    }



    void Start()
    {
        Camera cam = GetComponent<Camera>();
        _verticalSize = cam.orthographicSize;
        float ratio = (float)Screen.width / Screen.height;

        _horizontalSize = ratio * _verticalSize;
    }
}
