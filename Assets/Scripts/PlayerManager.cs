using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
    public GameObject zombie;
    public GameObject winPlace;

    [HideInInspector]
    public int countZombie =0;

    private void Start()
    {
        for(int i=0; i<zombie.transform.childCount; i++)
        {
            countZombie += zombie.transform.GetChild(i).childCount;
        }
        Debug.Log(countZombie);
    }

    private void Update()
    {
        if(countZombie <= 0)
        {
            winPlace.SetActive(true);
            Debug.Log("Win");
        }
    }
}
