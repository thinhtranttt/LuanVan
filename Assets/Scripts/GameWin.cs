using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Invoke("Win", 2f);
        
    }

    void Win()
    {
        SceneManager.LoadScene("destroyed_city");
    }
}
