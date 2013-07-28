using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public string CurrentMenu = "Main";
    

    string ServerName = "server name";

    void Start() {
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
    }

    void LobbyMenu() {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Back")) {
            Navigate("Main");
        }
    }

    void HostMenu() {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Back")) {
            Navigate("Main");
        } else if (GUI.Button(new Rect(10, 20 + 50, 200, 50), "Start Server")) {
            
        }

        GUI.Label(new Rect(10, 200, 200, 50), "Name:");
        ServerName = GUI.TextField(new Rect(100, 200, 200, 20), ServerName);
    }
}
