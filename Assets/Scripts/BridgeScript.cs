using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BridgeScript : MonoBehaviour
{
    void Update()
    {
        if( PhotonNetwork.CurrentRoom.PlayerCount == 2 )
        {
            gameObject.SetActive(false);
        }
    }
}
