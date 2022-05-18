using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    Text _text;


    void Awake()
    {
        GameController.onAsteroidDestroyed += OnAsteroidDestroyed;
    }


    void OnDestroy()
    {
        GameController.onAsteroidDestroyed -= OnAsteroidDestroyed;
    }

    void Update() 
    {
        _text.gameObject.SetActive(GameController.gameStarted);
    }


    void OnAsteroidDestroyed(int totalPoints) 
    {
        _text.text = string.Format("{0:00000}", totalPoints);
    }
}
