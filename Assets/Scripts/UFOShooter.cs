using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShooter : MonoBehaviour
{
    [SerializeField]
    float _period = 3f;

    [SerializeField]
    float _speed = 6f;

    [SerializeField]
    float _lifetime = 10f;

    [Header("Internal Setup")]
    [SerializeField]
    GameObject _bulletPrototype;



    IEnumerator Start()
    {
        while(true){
            yield return new WaitForSeconds(_period);
            if(GameController.currentShip!=null){
                GameObject obj = (GameObject) Instantiate(_bulletPrototype, transform.position, transform.rotation);
                Bullet bullet = obj.GetComponent<Bullet>();
                bullet.Shoot(_speed, _lifetime);
            }
        }
    }

    void Update() {
        if(GameController.currentShip != null) {
            transform.up = GameController.currentShip.transform.position  - transform.position;
        }
    }

}
