using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public GameObject cursorP1Prefab, cursorP2Prefab;
    private GameObject customCursorP1, customCursorP2;

    void Start()
    {
        Cursor.visible = false;
        if (PhotonNetwork.IsMasterClient)
        {
            customCursorP1 = PhotonNetwork.Instantiate(cursorP1Prefab.name, transform.position, transform.rotation);
        }
        else
        {
            customCursorP2 = PhotonNetwork.Instantiate(cursorP2Prefab.name, transform.position, transform.rotation);
        }
    }

    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        if (PhotonNetwork.IsMasterClient)
        {
            customCursorP1.transform.position = worldPosition;
        }
        else
        {
            customCursorP2.transform.position = worldPosition;
        }

    }
}
