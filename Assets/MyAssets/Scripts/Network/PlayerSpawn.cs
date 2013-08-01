using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

    public Object player;

    void OnServerInitialized() {
        Spawn();
    }

    void OnConnectedToServer() {
        Spawn();
    }

    void Spawn() {
        Object nw = Network.Instantiate(player, new Vector3(0.0f, 5.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), 0);
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
