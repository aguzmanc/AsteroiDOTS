using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    GameObject _text;

    void Start()
    {
        _text.SetActive(false);
        GameController.onLobby += OnLobby;
        GameController.onGameOver += OnGameOver;
    }


    void OnDestroy()
    {
        GameController.onLobby -= OnLobby;
        GameController.onGameOver -= OnGameOver;
    }


    void OnLobby() 
    {
        _text.SetActive(false);
    }


    void OnGameOver() 
    {
        StartCoroutine(RestartSequence());
    }


    IEnumerator RestartSequence() 
    {
        for(int i=0;i<4;i++) {
            _text.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _text.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        _text.SetActive(true);
        yield return new WaitForSeconds(2f);

        GameController.StartLobby();
    }
}
