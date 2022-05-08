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

    public List<DefaultRoom> defaultRooms;
    public string Name;

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
    }

    void Start()
    {
        PhotonNetwork.GameVersion = "1.0";
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        Debug.Log("서버에 연결을 시도합니다.");
        PhotonNetwork.ConnectUsingSettings(); // 서버연결
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버와 연결되었습니다.");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("대기실에 입장하였습니다.");

        InitiliazeRoom(0);
    }
    
    public void InitiliazeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(Name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 입장에 실패하였습니다.");
        base.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("새로운 플레이어가 입장하였습니다.");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnJoinedRoom()
    {
        Vector3 pos = new Vector3(-40f, 0f, -15f);
        Vector3 randPos = pos + Random.insideUnitSphere * 5;
        randPos.y = 0;

        //spawnedPlayerPrefab = PhotonNetwork.Instantiate(ChoiceCharacter.netPlayer, randPos, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        //PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}