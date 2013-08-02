using UnityEngine;
using System.Collections;
using System;

public class PlayerSpawn : MonoBehaviour {

    public GameObject playerObj;
    ArrayList playerScripts = new ArrayList();

    void OnServerInitialized() {
        Spawn(Network.player);
    }

    void OnConnectedToServer() {
        Spawn(Network.player);
    }

    void Spawn(NetworkPlayer pl) {
        int plnb = Convert.ToInt32(pl + "");
        GameObject go = (GameObject)Network.Instantiate(playerObj, new Vector3(0.0f, 1.0f, 0.0f), Quaternion.EulerAngles(0.0f, 0.0f, 0.0f), plnb);
        playerScripts.Add(go.GetComponent<PlayerControl>());
        go.networkView.RPC("SetPlayer", RPCMode.AllBuffered, pl);
    }


    void OnPlayerDisconnected(NetworkPlayer player) {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info) {
        Network.RemoveRPCs(Network.player);
        Network.DestroyPlayerObjects(Network.player);
        Application.LoadLevel(0);
    }

}
