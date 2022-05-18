using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShips : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    GameObject[] _images;

    void Start()
    {
        UpdateShips(0);

        GameController.onGameStarted += OnGameStarted;
        GameController.onShipDestroyed += OnShipDestroyed;
    }


    void OnDestroy() {
        GameController.onGameStarted -= OnGameStarted;
        GameController.onShipDestroyed -= OnShipDestroyed;
    }




    void OnGameStarted(int ships) {
        UpdateShips(ships);
    }

    void OnShipDestroyed(int ships) {
        UpdateShips(ships);
    }


    void UpdateShips(int ships) {
        for(int i=0;i<_images.Length;i++){
            if(i+1 > ships)
                _images[i].SetActive(false);
            else
                _images[i].SetActive(true);
        }
    }
}
