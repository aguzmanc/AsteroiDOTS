using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField]
    float _angularSpeed = 20;  // angles per second


    void Update()
    {
        transform.Rotate(0f, _angularSpeed * Time.deltaTime, 0f);
    }
}
