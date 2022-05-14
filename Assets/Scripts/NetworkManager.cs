using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public class DefaultRoom
    {
        public int maxPlayer;
    }

    public string roomName;
    public List<DefaultRoom> defaultRooms;
    [HideInInspector] public GameObject LocalPlayer;

    private readonly string _version = "1.0f";
    private string _userID = "SW";

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

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = _version;
        PhotonNetwork.NickName = _userID;
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings(); // 서버연결
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
        InitiliazeRoom(0);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"OnJoinRoomFailed: {returnCode}:{message}");
        InitiliazeRoom(0);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom: {PhotonNetwork.InRoom}");
        Debug.Log($"CurrentRoom: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"PlayerCount: {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}");
        }

        LocalPlayer =  PhotonNetwork.Instantiate("Player/Player", Vector3.zero, Quaternion.identity);
        LocalPlayer.gameObject.name = PhotonNetwork.NickName;
        if (LocalPlayer.GetComponent<PlayerController>() == null)
        LocalPlayer.AddComponent<PlayerController>();
        GameManager.Instance.player = LocalPlayer;
    }

    public void InitiliazeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
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
        base.OnLeftRoom();
        // PhotonNetwork.Destroy();
    }

    // public override void OnJoinedRoom()
    // {
    // Vector3 pos = new Vector3(-40f, 0f, -15f);
    // Vector3 randPos = pos + Random.insideUnitSphere * 5;
    // randPos.y = 0;

    // //spawnedPlayerPrefab = PhotonNetwork.Instantiate(ChoiceCharacter.netPlayer, randPos, Quaternion.identity);
    // }
}