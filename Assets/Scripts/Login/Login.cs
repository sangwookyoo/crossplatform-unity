using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Login : MonoBehaviour
{
    public InputField idInput;
    public Button createServerBtn;
    public Button joinedRoomBtn;

    void Start()
    {
        createServerBtn.onClick.AddListener(() =>
        {
            PhotonNetwork.NickName = idInput.text;
            NetworkManager.Instance.ConnectToServer();
            joinedRoomBtn.interactable = true;
        });

        joinedRoomBtn.onClick.AddListener(() =>
        {
            if (PhotonNetwork.IsConnectedAndReady) PhotonNetwork.LoadLevel("Main");
        });
    }
}
