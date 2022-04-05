using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private PlayerController _playerController;

    void Start()
    {
        LoadPlayerController();
    }

    void LoadPlayerController() {
        GameObject player = Instantiate(PlayerPrefab) as GameObject;
        _playerController = player.GetComponent<PlayerController>();
        _playerController.LoadingPlayer();
    }
}
