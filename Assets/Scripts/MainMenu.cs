using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject menu;
    public InputField roomName;
    public GameObject notice;

    private NetworkManager networkManager;

    private uint roomSize= 6;

    private void Start()
    {
        networkManager = NetworkManager.singleton;

        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void CreateRoom()
    {
        if (roomName.text != null && roomName.text != "")
        {
            UnityEngine.XR.XRSettings.enabled = true;
            Debug.Log("Create room " + roomName.text + " with " + roomSize + " player");
            networkManager.matchMaker.CreateMatch(roomName.text, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            menu.SetActive(false);
        }
        else
        {
            notice.SetActive(true);
        }
    }


}
