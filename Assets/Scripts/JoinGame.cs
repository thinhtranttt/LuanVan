using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour {

    private List<GameObject> roomList = new List<GameObject>();

    private NetworkManager networkManager;

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;
    // Use this for initialization
    void Start () {

        UnityEngine.XR.XRSettings.enabled = false;
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "",true,0,0, OnMatchList); 
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";

        if (!success || matchList == null)
        {
            status.text = "Couldn't get room list.";
            return;
        }
        ClearRoomList(); 
        foreach (MatchInfoSnapshot match in matchList)
        {
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
            _roomListItemGO.transform.SetParent(roomListParent);
            
            RoomItem _roomListItem = _roomListItemGO.GetComponent<RoomItem>();
            if (_roomListItem != null)
            {
                _roomListItem.SetUp(match,JoinRoom);
                Debug.Log("Create");
            }


            //// as well as setting up a callback function that will join the game.

            roomList.Add(_roomListItemGO);
        }

        if (roomList.Count == 0)
        {
            status.text = "No rooms at the moment.";
        }

        
    }

    public void ClearRoomList()
    {
        for(int i = 0;i<roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        networkManager.matchMaker.JoinMatch(_match.networkId, "","","",0,0, networkManager.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining...";
    }
}
