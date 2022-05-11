using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour
{
    [SerializeField]
    GameObject _asteroidPrototype;
    [SerializeField]
    [Range(1,999999)]
    int _totalAsteroids=100;

    IEnumerator Start()
    {
        Debug.Log("generation in 3...");
        yield return new WaitForSeconds(3f);

        float initTime = Time.realtimeSinceStartup;
        Debug.Log("init time: " + Time.time);
        for(int i=0;i<_totalAsteroids;i++)
            GenerateAsteroid();

        float diff = (Time.realtimeSinceStartup - initTime);

        Debug.Log("end time: " + Time.realtimeSinceStartup);
        Debug.Log(string.Format("TimeGenerated: {0}", diff));
    }


    void GenerateAsteroid() 
    {
        Vector3 pos = Random.onUnitSphere * Random.Range(1f, 5f);
        Instantiate(_asteroidPrototype, pos, Quaternion.identity);
    }
}
