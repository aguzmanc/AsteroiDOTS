using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPushStartButton : MonoBehaviour
{
    [Header("Internal Setup")]
    [SerializeField]
    GameObject _text;

    void Awake() {
        GameController.onLobby += OnLobby;
    }

    void OnDestroy() {
        GameController.onLobby -= OnLobby;
    }


    void OnLobby()
    {
        StartCoroutine(LobbySequence());
    }


    IEnumerator LobbySequence()
    {
        while(GameController.inLobby) {
            _text.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _text.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        if(GameController.inLobby) {
            if(Input.GetKeyDown(KeyCode.Return)){
                _text.SetActive(false);
                GameController.StartGame();
            }
        }
    }
}
