using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace tutoriales.multiplayer
{
    public class AutoLobby : MonoBehaviourPunCallbacks
    {
        public Button ConnectButton;
        public Button JoinRandomButton;
        public int playersCount;
        public Text playerCount;

        public Text Log;
        public byte maxPlayerperRoom = 4;
        public byte minPlayerperRoom = 1;

        private bool isLoading = false;

        public void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {

                if (PhotonNetwork.ConnectUsingSettings())
                {
                    Log.text += "\nConnected to Server";
                }
                else
                {
                    Log.text += "\nFailing connecting to Server";
                }
            }
        }

        public override void OnConnectedToMaster()
        {
            ConnectButton.interactable = false;
            JoinRandomButton.interactable = true;
        }

        public void JoinRandom()
        {
            if (PhotonNetwork.JoinRandomRoom())
            {

                Log.text += "\nFail joining room";
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Log.text += "\nNo Room to joing. Creating one...";

            if (PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = maxPlayerperRoom }))
            {
                Log.text += "\nRoom Created";

            }
            else
            {
                Log.text += "\nFail creating room";
            }
        }

        public override void OnJoinedRoom()
        {
            Log.text += "\nJoinned";
            JoinRandomButton.interactable = false;
        }

        private void FixedUpdate()
        {
            if(PhotonNetwork.CurrentRoom != null)
            {
                playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
                playerCount.text = playersCount + "/" + maxPlayerperRoom;

            }

            if(isLoading == false && playersCount >= minPlayerperRoom)
            {
                LoadMap();
            }


        }

        private void LoadMap()
        {
            isLoading = true;
            PhotonNetwork.LoadLevel(1);
        }


    }
}