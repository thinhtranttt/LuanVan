using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Minimap : NetworkBehaviour {

    public Transform player;

	void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
