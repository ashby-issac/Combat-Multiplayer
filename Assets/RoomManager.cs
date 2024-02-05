using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject[] spawnPoint;

    int index = 0;
    PhotonView photonView;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        photonView = GetComponent<PhotonView>();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log($":: Connected to master server JoinLobby()");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log($":: JoinedLobby, creating room...");
        PhotonNetwork.JoinOrCreateRoom("Test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (photonView.IsMine)
        {
            var playerInstance = PhotonNetwork.Instantiate(player1.name, spawnPoint[0].transform.position, Quaternion.identity);
        }
        else
        {
            var playerInstance = PhotonNetwork.Instantiate(player2.name, spawnPoint[1].transform.position, Quaternion.identity);
        }
    }
}
