using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Makes sure no near asteroid is in the area of instantiation of the ship,
   to avoid being destroyed when it appears*/
public class ShipCreator : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    HitDetector _asteroidDetector;

    [SerializeField]
    GameObject _shipPrototype;

    public void CreateShip() {
        StartCoroutine(ShipCreation());
    }


    IEnumerator ShipCreation() {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitWhile(()=>(_asteroidDetector.objectsDetected>0));
        // creates the ship
        Instantiate(_shipPrototype, Vector3.zero, Quaternion.identity);
    }

}
