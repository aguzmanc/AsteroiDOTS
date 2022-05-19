using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    Text _text;


    void Awake()
    {
        GameController.onGameStarted += OnGameStarted;
        GameController.onAsteroidDestroyed += OnAsteroidDestroyed;
    }


    void OnDestroy()
    {
        GameController.onGameStarted -= OnGameStarted;
        GameController.onAsteroidDestroyed -= OnAsteroidDestroyed;
    }

    void Update() 
    {
        _text.gameObject.SetActive(GameController.gameStarted);
    }



    void OnGameStarted(int ships) {
        _text.text = "00000";
    }

    void OnAsteroidDestroyed(int totalPoints) 
    {
        _text.text = string.Format("{0:00000}", totalPoints);
    }
}
