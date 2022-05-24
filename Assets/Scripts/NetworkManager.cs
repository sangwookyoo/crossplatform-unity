using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string roomName;
    public byte maxPlayer;

    private readonly string _gameVersion = "1.0";

    /* Singleton */
    private static NetworkManager _instance;

    public static NetworkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NetworkManager();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        else
        {
            Destroy(this);
        }

        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conneted to Server");
        Debug.Log("Server Rate: " + PhotonNetwork.SendRate);
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby: {PhotonNetwork.InLobby}");
        InitRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom: {PhotonNetwork.InRoom}");
        Debug.Log($"CurrentRoom: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"PlayerCount: {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"PlayerName: {player.Value.NickName},{player.Value.ActorNumber}");
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"OnJoinRoomFailed: {returnCode}:{message}");
        InitRoom();
    }

    public void InitRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayer;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"New player Entered: {newPlayer.NickName},{newPlayer.ActorNumber}");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Destroy(GameManager.Instance.player);
    }
}