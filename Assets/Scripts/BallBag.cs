using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class BallBag : MonoBehaviour
{
    public int numBalls;
    public GameObject ballP1, ballP2;
    public float minY, maxY;
    public float minXP1, maxXP1;
    public float minXP2, maxXP2;

    private List<Vector3> initialPositionBalls;
    private List<Transform> balls;

    public void Awake()
    {
        balls = new List<Transform>();
        initialPositionBalls = new List<Vector3>();

        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < numBalls; i++)
            {
                Vector2 posBall = new Vector2(Random.Range(minXP1, maxXP1), Random.Range(minY, maxY));
                GameObject newBall = PhotonNetwork.Instantiate(ballP1.name, posBall, quaternion.identity);
                balls.Add(newBall.transform);
                initialPositionBalls.Add(newBall.transform.position);
            }
        }
        else
        {
            for (int i = 0; i < numBalls; i++)
            {
                Vector2 posBall = new Vector2(Random.Range(minXP2, maxXP2), Random.Range(minY, maxY));
                GameObject newBall = PhotonNetwork.Instantiate(ballP2.name, posBall, quaternion.identity);
                balls.Add(newBall.transform);
                initialPositionBalls.Add(newBall.transform.position);
            }
        }
    }


    public void OnMouseDown()
    {
        Recolocate();
    }

    public void Recolocate()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].position = initialPositionBalls[i];
        }
    }
}
