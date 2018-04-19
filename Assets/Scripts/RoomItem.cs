using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
    private JoinRoomDelegate joinRoomDelegate;

    [SerializeField]
    private Text roomNameTxt;
    private MatchInfoSnapshot matchInfo;

    public void SetUp(MatchInfoSnapshot match, JoinRoomDelegate joinRoom)
    {
        matchInfo = match;
        joinRoomDelegate = joinRoom;
        roomNameTxt.text = matchInfo.name;

    }

    public void JoinGame()
    {
        GameObject.FindGameObjectWithTag("Menu").SetActive(false);
        joinRoomDelegate.Invoke(matchInfo);
        UnityEngine.XR.XRSettings.enabled = true;
    }
}
