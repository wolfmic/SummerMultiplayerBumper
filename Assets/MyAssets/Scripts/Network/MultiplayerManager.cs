using UnityEngine;
using System.Collections;

public class MultiplayerManager : MonoBehaviour {

    public int maxplayers = 4;

    string servername;

    public void StartServer() {
        Network.InitializeServer(maxplayers, 25003, false);
    }

    void OnServerInitialized() {
        Debug.Log("Hello");
    }

}
