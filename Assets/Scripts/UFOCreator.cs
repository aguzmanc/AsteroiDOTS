using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCreator : MonoBehaviour
{
    Coroutine _sequence;


    [SerializeField]
    float _minUfoTime = 3;

    [SerializeField]
    float _maxUfoTime = 10;


    [Header("Internal Setup")]
    [SerializeField]
    GameObject _ufoPrototype;


    void Start()
    {
        GameController.onGameStarted += OnGame;
        GameController.onGameOver += OnEndGame;
    }

    

    void OnGame(int ships) {
        if(_sequence!=null)
            StopCoroutine(_sequence);
        
        _sequence = StartCoroutine(_GameSequence());
    }
    

    void OnEndGame()
    {
        if(_sequence!=null)
            StopCoroutine(_sequence);
    }


    IEnumerator _GameSequence() {
        yield return new WaitUntil(() => GameController.gameStarted);
        while(true){
            float waitTime = Random.Range(_minUfoTime, _maxUfoTime);
            yield return new WaitForSeconds(waitTime);

            float x = Random.Range(ScreenLimits.leftLimit, ScreenLimits.rightLimit);
            float y = Random.Range(ScreenLimits.downLimit, ScreenLimits.upLimit);
            Instantiate(_ufoPrototype, new Vector3(x,y,0), Quaternion.identity);
        }
    }
}
