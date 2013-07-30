using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public string CurrentMenu = "Main";

    GameObject mm;
    

    string ServerName = "server name";
    string playername = "Toto";
    string serverip = "127.0.0.1";

    void Start() {
        mm = GameObject.Find("NetworkObject");
    }

    void OnGUI() {
        if (CurrentMenu == "Main") {
            MainMenu();
        } else if (CurrentMenu == "Lobby") {
            LobbyMenu();
        } else if (CurrentMenu == "Host") {
            HostMenu();
        }
    }

    void Navigate(string s) {
        CurrentMenu = s;
    }

    void MainMenu() {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Host Game")) {
            Navigate("Host");
        } else if (GUI.Button(new Rect(10, 20 + 50, 200, 50), "Join")) {
            Navigate("Lobby");
        }
        GUI.Label(new Rect(10, 30 + 150, 200, 50), "Player name : ");
        playername = GUI.TextField(new Rect(10, 200, 200, 20), playername);
    }

    void LobbyMenu() {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Back")) {
            Navigate("Main");
        } else if (GUI.Button(new Rect(10, 20 + 100, 200, 50), "Start")) {
            Network.Connect(serverip, 25000);
        }
        GUI.Label(new Rect(10, 30 + 150, 200, 50), "IP Server : ");
        serverip = GUI.TextField(new Rect(10, 200, 200, 20), serverip);

    }

    void HostMenu() {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Back")) {
            Navigate("Main");
        } else if (GUI.Button(new Rect(10, 20 + 50, 200, 50), "Start Server")) {
            mm.GetComponent<MultiplayerManager>().StartServer(ServerName);
        }

        GUI.Label(new Rect(10, 200, 200, 50), "Name:");
        ServerName = GUI.TextField(new Rect(100, 200, 200, 20), ServerName);
    }
}
