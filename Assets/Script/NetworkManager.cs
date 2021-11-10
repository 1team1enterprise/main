using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room" + Random.Range(0, 2), new RoomOptions { MaxPlayers = 2 }, null);

    public override void OnJoinedRoom()
    {

        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

    }
}