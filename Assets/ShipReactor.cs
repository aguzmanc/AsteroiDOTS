using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipReactor : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 1f)]
    float _period = 0.1f;

    [Header("Internal Setup")]
    [SerializeField]
    GameObject _mesh;


    Ship _ship;
    float _timer;


    void Awake() {
        _ship = GetComponentInParent<Ship>();
        _timer = _period;
    }

    void Update()
    {
        if(_ship.withImpulse==false)
            _mesh.SetActive(false);
        else {
            _timer -= Time.deltaTime;
            if(_timer<=0){
                _timer = _period;
                _mesh.SetActive(!_mesh.activeSelf);
            }
        }
    }
}
