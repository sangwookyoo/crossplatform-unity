using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Billboard : MonoBehaviourPunCallbacks
{
    private Text _nickName;

    void Start()
    {
        if (!PhotonNetwork.IsConnected) return;
        _nickName = GetComponent<Text>();
        _nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}