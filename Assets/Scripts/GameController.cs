using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController _instance;

    bool _gameStarted = false;
    bool _gameOver = false;

    [SerializeField]
    GameObject _shipPrototype;


    public static System.Action onGameStarted;
    public static System.Action onGameOver;
    public static bool gameStarted => _instance._gameStarted;
    public static bool gameOver => _instance._gameOver;



    public static void StartGame() 
    {
        _instance._gameStarted = true;
        _instance._gameOver = false;

        // creates the ship
        Instantiate(_instance._shipPrototype, Vector3.zero, Quaternion.identity);

        if(onGameStarted!=null)
            onGameStarted();
    }


    public static void GameOver() 
    {
        _instance._gameStarted = true;
        _instance._gameOver = true;

        if(onGameOver != null)
            onGameOver();
    }


    void Awake()
    {
        _instance = this;
    }

}
