using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    // Enemy Prefab
    public GameObject enemyPrefab;

    // Number of enemies
    public int numberOfEnemies = 0;

    // Spawning zone
    public int zone = 1;

    // Initiate zone for spawning
    float x1;
    float x2;
    float z1;
    float z2;
    float y;
    private void SetUpZone()
    {
        switch (zone)
        {
            case 1:
                x1 = 900;
                x2 = 1061;
                z1 = 187;
                z2 = 596;
                break;
            case 2:
                x1 = 129;
                x2 = 806;
                z1 = 71;
                z2 = 469;
                break;
            default:
                x1 = 129;
                x2 = 806;
                z1 = 71;
                z2 = 469;
                break;
        }
        y = 0;
    }

    // spawn enemy
    public override void OnStartServer()
    {
        // Setup zone information
        SetUpZone();

        // Spawn a number of enemies
        for(int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(x1, x2),
                y,
                Random.Range(z1, z2));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}
