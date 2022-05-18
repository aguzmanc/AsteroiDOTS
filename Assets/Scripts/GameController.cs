using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController _instance;

    bool _gameStarted = false;
    bool _gameOver = false;
    int _points;

    [SerializeField]
    GameObject _shipPrototype;

    [SerializeField]
    [Range(0f, 1000f)]
    int _pointsPerBigAsteroid = 20;

    [SerializeField]
    [Range(0f, 1000f)]
    int _pointsPerMediumAsteroid = 40;

    [SerializeField]
    [Range(0f, 1000f)]
    int _pointsPerSmallAsteroid = 70;


    public static System.Action<int> onAsteroidDestroyed;
    public static System.Action onGameStarted;
    public static System.Action onGameOver;
    public static bool gameStarted => _instance._gameStarted;
    public static bool gameOver => _instance._gameOver;



    public static void StartGame() 
    {
        _instance._gameStarted = true;
        _instance._gameOver = false;
        _instance._points = 0;

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


    public static void NotifyAsteroidDestroyed(Asteroid.AsteroidType type)
    {
        _instance._NotifyAsteroidDestroyed(type);
    }


    


    void Awake()
    {
        _instance = this;
    }



    void _NotifyAsteroidDestroyed(Asteroid.AsteroidType type) 
    {
        if(type==Asteroid.AsteroidType.Big)
            _points += _pointsPerBigAsteroid;
        else if(type==Asteroid.AsteroidType.Medium)
            _points += _pointsPerMediumAsteroid;
        else if(type==Asteroid.AsteroidType.Small)
            _points += _pointsPerSmallAsteroid;

        if(onAsteroidDestroyed!=null){
            if(type==Asteroid.AsteroidType.Big)
                onAsteroidDestroyed(_points);
            else if(type==Asteroid.AsteroidType.Medium)
                onAsteroidDestroyed(_points);
            else if(type==Asteroid.AsteroidType.Small)
                onAsteroidDestroyed(_points);
        }
    }
}
