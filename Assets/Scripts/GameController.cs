using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController _instance;
    const int LIFES_BEFORE_OVER = 3;

    bool _gameStarted = false;
    bool _gameOver = false;
    int _points;
    int _lifesRemain;

    [SerializeField]
    [Range(0f, 1000f)]
    int _pointsPerBigAsteroid = 20;

    [SerializeField]
    [Range(0f, 1000f)]
    int _pointsPerMediumAsteroid = 40;

    [SerializeField]
    [Range(0f, 1000f)]
    int _pointsPerSmallAsteroid = 70;

    [Header("Internal Setup")]
    [SerializeField]
    ShipCreator _shipCreator;


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
        _instance._lifesRemain = LIFES_BEFORE_OVER;

        _instance._shipCreator.CreateShip();

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

    public static void NotifyShipDestroyed()
    {
        _instance._NotifyShipDestroyed();
    }


    public static void NotifyAsteroidDestroyed(Asteroid.AsteroidType type)
    {
        _instance._NotifyAsteroidDestroyed(type);
    }


    


    void Awake()
    {
        _instance = this;
    }


    void _NotifyShipDestroyed() {
        _lifesRemain --;
        if(_lifesRemain > 0) {

        } else 
            GameOver();
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
