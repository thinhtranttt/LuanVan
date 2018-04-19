using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.VR;

public class MainMenu1 : MonoBehaviour {

    public GameObject menu;
    public InputField input;

    private Socket socket;

    public void JoinLan()
    {
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.networkAddress = input.text;
        
        NetworkManager.singleton.StartClient();
        UnityEngine.XR.XRSettings.enabled = true;
    }

    private void Start()
    {
        UnityEngine.XR.XRSettings.enabled = false;

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        input.text = GetLocalIP().Remove(GetLocalIP().LastIndexOf('.') + 1) + "1";
    }

    private string GetLocalIP()
    {
        IPHostEntry host;
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(IPAddress ip in host.AddressList)
        {
            if(ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "127.0.0.1";
    }


}
