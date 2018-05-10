using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    PlayerManager playerManager;
    public PlayerController player1;
    public PlayerController player2;
    public float center { get; internal set; }
    public Vector3 boxExtents;

    void Awake()
    {
        if (ServiceLocator.instance.cam == null)
        {
            ServiceLocator.instance.cam = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        playerManager = ServiceLocator.instance.playerManager;
        player1 = playerManager.PlayerOne;
        player2 = playerManager.PlayerTwo;
    }

    void Update()
    {
        CenterCamera();
    }

    void CenterCamera()
    {
        center = Vector3.Distance(new Vector3(player1.transform.position.x, 0, 0), new Vector3(player2.transform.position.x, 0, 0));
        center /= 2;
        if (player1.transform.position.x < player2.transform.position.x)
        {
            transform.position = new Vector3(player1.transform.position.x + center, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player2.transform.position.x + center, transform.position.y, transform.position.z);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxExtents);
    }
}
