using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPushStartButton : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    GameObject _text;


    IEnumerator Start()
    {
        while(GameController.gameStarted==false) {
            _text.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _text.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        if(GameController.gameStarted == false) {
            if(Input.GetKeyDown(KeyCode.Return)){
                _text.SetActive(false);
                GameController.StartGame();
            }
        }
    }
}
