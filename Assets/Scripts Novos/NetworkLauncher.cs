using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Supervisory;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public bool InternetConnected = false;
    public bool MasterServerConnected = false;
    public bool host = false;
    public bool client = false;
    public bool ConnectingToMasterServer = false;
    public bool CreatingARoom = false;
    public GameObject CentralObject;
    public Aplication Central;
    public float UpdateTime;
    public float UpdateInterval = 3f;
    public List<RoomInfo> AllRooms;

    void Start()
    {
        Central = CentralObject.GetComponent<Aplication>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= UpdateTime)
        {
            if (Central.ThisProcess.WebConnection.Connected && !InternetConnected)
            {
                InternetConnected = true;
            }

            if (InternetConnected && !MasterServerConnected && !ConnectingToMasterServer)
            {
                NetworkConnect();
                ConnectingToMasterServer = true;
            }

            if ((Central.ThisProcess.CableConnection.Connected || Central.ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) && InternetConnected && MasterServerConnected && !host && !client && !CreatingARoom)
            {
                if (!RoomExists(AllRooms, Central.ThisProcess.Name))
                {
                    CreateRoom();
                    CreatingARoom = true;
                }
            }
            if (!(Central.ThisProcess.CableConnection.Connected || Central.ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) && InternetConnected && MasterServerConnected && !host && !client && !CreatingARoom)
            {
                if (RoomExists(AllRooms, Central.ThisProcess.Name))
                {
                    JoinRoom();
                }
            }

            //Verificar se ainda esta conectado, se não, desabilitar host e client


            UpdateTime += UpdateInterval;
        }
    }

    public bool RoomExists(List<RoomInfo> roomList, string Name)
    {
        bool Exists = false;
        foreach (RoomInfo info in roomList)
        {
            if (info.Name == Name)
            {
                Exists = true;
            }
        }
        return Exists;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(Central.ThisProcess.Name);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(Central.ThisProcess.Name, new RoomOptions { MaxPlayers = 20 }, null);
    }

    public void NetworkConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Functions.LogInfo("Desconectado: " + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        Functions.LogInfo("Conectado ao servidor mestre");
        MasterServerConnected = true;
        ConnectingToMasterServer = false;
    }

    public override void OnCreatedRoom()
    {
        CreatingARoom = false;
        host = true;
    }

    public override void OnJoinedRoom()
    {
        Functions.LogInfo("Conectado a sala " + Central.ThisProcess.Name);
        gameObject.GetComponent<NetworkCommunication>().AskFirstReceiveFromMaster = true; //Pedido de sincronização
        if (!host) client = true;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Functions.LogInfo("A conexão a sala falhou " + returnCode + " Mensagem " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllRooms = roomList;
        //UnityEngine.Debug.Log("Tamanho da lista de rooms: " + roomList.Count.ToString());
    }

}
