using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour {

    public int maxplayers = 4;
    public List<MPPlayer> playerlist = new List<MPPlayer>();

    string servername;

    void update() {
        foreach (MPPlayer t in playerlist) {
            print(t.playername);
        }
    }

    public void StartServer(string servername) {
        Network.InitializeServer(maxplayers, 25003, false);
    }

    void OnServerInitialized() {
        Server_PlayerJoinRequest("", Network.player);
    }

    void OnConnectedToServer() {
        networkView.RPC("Server_PlayerJoinRequest", RPCMode.Server);
    }

    void OnPlayerDisconnected(NetworkPlayer id) {
        networkView.RPC("Client_RemovePlayer", RPCMode.All, id);
    }

    [RPC]
    void Server_PlayerJoinRequest(string playername, NetworkPlayer view) {
        networkView.RPC("Client_AddPlayerToList", RPCMode.All, playername, view);
    }

    [RPC]
    void Client_AddPlayerToList(string playername, NetworkPlayer view) {
        MPPlayer TmpPlayer = new MPPlayer(playername, view);
        playerlist.Add(TmpPlayer);
    }

    [RPC]
    void Client_RemovePlayer(NetworkPlayer view) {
        MPPlayer tmp = null;
        foreach (MPPlayer t in playerlist) {
            if (t.PlayerNetwork == view) {
                tmp = t;
            }
        }
        if (tmp != null) {
            playerlist.Remove(tmp);
        }

    }

}

public class MPPlayer {

    public string playername;
    public NetworkPlayer PlayerNetwork;

    public MPPlayer(MPPlayer p) {
        playername = p.playername;
    }

    public MPPlayer(string tmpname, NetworkPlayer tmpview) {
        playername = tmpname;
        PlayerNetwork = tmpview;
    }
}