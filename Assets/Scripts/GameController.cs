using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController _instance;
    const int LIFES_BEFORE_OVER = 3;

    bool _inLobby = true;
    bool _gameStarted = false;
    bool _gameOver = false;
    int _points;
    int _shipsRemain;
    int _currentAsteroidsCount;

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


    public static System.Action<int> onGameStarted;
    public static System.Action onLobby;
    public static System.Action onAllAsteroidsDestroyed;
    public static System.Action<int> onAsteroidDestroyed;
    public static System.Action<int> onShipDestroyed;
    public static System.Action onGameOver;
    public static bool inLobby => _instance._inLobby;
    public static bool gameStarted => _instance._gameStarted;
    public static bool gameOver => _instance._gameOver;


    public static void StartLobby() 
    {
        _instance._inLobby = true;
        _instance._gameStarted = false;
        _instance._gameOver = false;

        if(onLobby != null)
            onLobby();
    }



    public static void StartGame() 
    {
        _instance._inLobby = false;
        _instance._gameStarted = true;
        _instance._gameOver = false;
        _instance._points = 0;
        _instance._shipsRemain = LIFES_BEFORE_OVER;

        _instance._shipCreator.CreateShip();

        if(onGameStarted!=null)
            onGameStarted(_instance._shipsRemain);
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


    public static void NotifyAsteroidCreated()
    {
        _instance._currentAsteroidsCount ++;
    }


    public static void NotifyAsteroidDestroyed(Asteroid.AsteroidType type)
    {
        _instance._currentAsteroidsCount --;
        _instance._NotifyAsteroidDestroyed(type);

        if(_instance._currentAsteroidsCount==0) {
            if(onAllAsteroidsDestroyed!=null) 
                onAllAsteroidsDestroyed();
        }
    }




    


    void Awake()
    {
        _instance = this;
    }


    IEnumerator Start() 
    {
        yield return new WaitForSeconds(1f);
        StartLobby();
    }


    void _NotifyShipDestroyed() {
        _shipsRemain --;

        if(onShipDestroyed!=null)
            onShipDestroyed(_shipsRemain);

        if(_shipsRemain > 0) {
            _instance._shipCreator.CreateShip();
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
