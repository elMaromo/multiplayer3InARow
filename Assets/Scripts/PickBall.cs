using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class PickBall : MonoBehaviour
{
    private bool grabbed;
    private bool placed;
    private Rigidbody2D rb;
    private PhotonView view;

    private void Awake()
    {
        placed = false;
        grabbed = false;
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    private void OnMouseDown()
    {
        if (view.IsMine && !placed)
        {
            grabbed = true;
        }
    }

    private void OnMouseUp()
    {
        grabbed = false;
    }

    private void Update()
    {
        if (grabbed)
        {
            rb.velocity = Vector2.zero;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z = 0;

            //transform.position = worldPosition;

            //Vector3 newVelocity = worldPosition - transform.position;
            //rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, 2 * Time.deltaTime);
            rb.velocity = (worldPosition - transform.position) * 20;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.gameObject.CompareTag("NoGrab"))
        {
            grabbed = false;
        }

        if (col.transform.gameObject.CompareTag("Placed"))
        {
            placed = true;
            grabbed = false;
        }

        if (col.transform.gameObject.CompareTag("NewGameIndicator"))
        {
            placed = false;
            grabbed = false;
        }
    }
}
