using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Login : MonoBehaviourPunCallbacks
{
    public InputField idInputField;
    public InputField pwInputField;
    public Button siginBtn;

    void Start()
    {
        siginBtn.onClick.AddListener(() =>
        {
            PhotonNetwork.NickName = idInputField.text;
            NetworkManager.Instance.ConnectToServer();
        });
    }
}
