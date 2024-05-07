using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{
    public bool P1Token, P2Token;
    public LayerMask layerTokens;
    public string P1tag, P2tag;

    private void Update()
    {
        Scan();
    }

    void Scan()
    {
        P1Token = false;
        P2Token = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.CompareTag(P1tag))
            {
                P1Token = true;
            }

            if (hit.transform.gameObject.CompareTag(P2tag))
            {
                P2Token = true;
            }
        }

    }

}

