using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class MyLobbyHook : LobbyHook { 

        public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) {
            gamePlayer.name = lobbyPlayer.GetComponent<LobbyPlayer>().playerName;
        }
    }
}
