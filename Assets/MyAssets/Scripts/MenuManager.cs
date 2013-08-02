using UnityEngine;
using System.Collections;
using System;

public class MenuManager : MonoBehaviour {

    MultiplayerManager mm;
    string ip = "127.0.0.1";
    int port = 20000;

    void Start() {
        mm = GameObject.Find("NetworkObject").GetComponent<MultiplayerManager>();
    }

    void Update() {
    }

    void OnGUI() {
        if (Network.peerType == NetworkPeerType.Disconnected) {
            GUILayout.Label("Not Connected");

            ip = GUILayout.TextField(ip, GUILayout.MinWidth(100));
            port = Convert.ToInt32(GUILayout.TextField(port.ToString()));

            GUILayout.BeginVertical();
            if (GUILayout.Button("Connect as Client")) {
                Application.LoadLevel(2);
                mm.Connect(ip, port);
            }
            if (GUILayout.Button("Start Server")) {
                if (Application.loadedLevel != 2) {
                    Application.LoadLevel(2);
                }
                mm.StartHost(32, port);
            }
            GUILayout.EndVertical();
        } else {
            if (Network.peerType == NetworkPeerType.Connecting) {
                GUILayout.Label("Connection status: Connecting");
            } else if (Network.peerType == NetworkPeerType.Client) {
                GUILayout.Label("Connection status: Client");
                GUILayout.Label("Ping:" + Network.GetAveragePing(Network.connections[0]));
            } else if (Network.peerType == NetworkPeerType.Server) {
                GUILayout.Label("Connection status: Server");
                if (Network.connections.Length >= 1) {
                    GUILayout.Label("Ping to first player: " + Network.GetAveragePing(Network.connections[0]));
                }
            }

            if (GUILayout.Button("Disconnect")) {
                Application.LoadLevel(0);
                Network.Disconnect(200);
            }
        }
    }
}