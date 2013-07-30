using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour {

    public void StartHost(int players, int port) {
        Network.InitializeServer(players, port, false);
    }

    public void Connect(string ip, int port) {
        Network.Connect(ip, port);
    }
}