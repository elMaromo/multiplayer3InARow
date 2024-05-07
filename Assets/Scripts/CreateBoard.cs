using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    public float bottom, top, left, right, winTime, checkWinTime;
    public GameObject detector, P1Win, P2Win;
    public List<List<DetectorScript>> detectors;
    public BallBag ballGenerator;

    private int maxTopNumber, maxRightNumber;
    private float timerWin, timerCheckWin1, timerCheckWin2;
    private bool someOneWon;
    private AudioSource audio;

    private void Awake()
    {
        maxTopNumber = 6;
        maxRightNumber = 7;
        timerWin = 0;
        timerCheckWin1 = 0;
        timerCheckWin2 = 0;
        someOneWon = false;
        detectors = new List<List<DetectorScript>>();
        InitializeBoard();

        audio = GetComponent<AudioSource>();

        Debug.Log(detectors[0].Count);
        Debug.Log(detectors.Count);
    }

    private void InitializeBoard()
    {
        float xIteration = (right - left) / maxRightNumber;
        float yIteration = (top - bottom) / maxTopNumber;

        for (int i = 0; i < maxTopNumber; i++)
        {
            detectors.Add(new List<DetectorScript>());

            for (int j = 0; j < maxRightNumber; j++)
            {
                Vector3 posNextTile = Vector3.zero;
                posNextTile.x = left + (j * xIteration);
                posNextTile.y = bottom + (i * yIteration);
                posNextTile.z = 1;
                GameObject nextTile = Instantiate(detector, posNextTile, transform.rotation);
                detectors[i].Add(nextTile.GetComponent<DetectorScript>());
            }
        }
    }


    private void Update()
    {
        if (someOneWon)
        {
            CelebrateAndReset();
        }
        else
        {
            if (P1Won())
            {
                timerCheckWin1 += Time.deltaTime;

                if (timerCheckWin1 > checkWinTime)
                {
                    Debug.Log("1 gana ");
                    P1Win.SetActive(true);
                    someOneWon = true;
                    audio.Play();
                }
            }
            else
            {
                timerCheckWin1 = 0;
            }

            if (P2Won())
            {
                timerCheckWin2 += Time.deltaTime;

                if (timerCheckWin2 > checkWinTime)
                {
                    Debug.Log("2 gana ");
                    P2Win.SetActive(true);
                    someOneWon = true;
                    audio.Play();
                }
            }
            else
            {
                timerCheckWin2 = 0;
            }
        }

    }

    public void CelebrateAndReset()
    {
        timerWin += Time.deltaTime;

        if (timerWin > winTime)
        {
            P1Win.SetActive(false);
            P2Win.SetActive(false);
            ballGenerator.Recolocate();
        }

        if (timerWin > winTime + 1)
        {
            someOneWon = false;
            timerWin = 0;
        }
    }

    public bool P1Won()
    {
        // horizontalCheck 
        for (int i = 0; i < maxTopNumber; i++)
        {
            for (int j = 0; j < maxRightNumber - 3; j++)
            {
                if (detectors[i][j].P1Token && detectors[i][j + 1].P1Token && detectors[i][j + 2].P1Token && detectors[i][j + 3].P1Token)
                {
                    return true;
                }
            }
        }

        // verticalCheck
        for (int i = 0; i < maxTopNumber - 3; i++)
        {
            for (int j = 0; j < maxRightNumber; j++)
            {
                if (detectors[i][j].P1Token && detectors[i + 1][j].P1Token && detectors[i + 2][j].P1Token && detectors[i + 3][j].P1Token)
                {
                    return true;
                }
            }
        }
        // ascendingDiagonalCheck 
        for (int i = 3; i < maxTopNumber; i++)
        {
            for (int j = 0; j < maxRightNumber - 3; j++)
            {
                if (detectors[i][j].P1Token && detectors[i - 1][j + 1].P1Token && detectors[i - 2][j + 2].P1Token && detectors[i - 3][j + 3].P1Token)
                {
                    return true;
                }
            }
        }

        // descendingDiagonalCheck
        for (int i = 3; i < maxTopNumber; i++)
        {
            for (int j = 3; j < maxRightNumber; j++)
            {
                if (detectors[i][j].P1Token && detectors[i - 1][j - 1].P1Token && detectors[i - 2][j - 2].P1Token && detectors[i - 3][j - 3].P1Token)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool P2Won()
    {
        // horizontalCheck 
        for (int i = 0; i < maxTopNumber; i++)
        {
            for (int j = 0; j < maxRightNumber - 3; j++)
            {
                if (detectors[i][j].P2Token && detectors[i][j + 1].P2Token && detectors[i][j + 2].P2Token && detectors[i][j + 3].P2Token)
                {
                    return true;
                }
            }
        }

        // verticalCheck
        for (int i = 0; i < maxTopNumber - 3; i++)
        {
            for (int j = 0; j < maxRightNumber; j++)
            {
                if (detectors[i][j].P2Token && detectors[i + 1][j].P2Token && detectors[i + 2][j].P2Token && detectors[i + 3][j].P2Token)
                {
                    return true;
                }
            }
        }
        // ascendingDiagonalCheck 
        for (int i = 3; i < maxTopNumber; i++)
        {
            for (int j = 0; j < maxRightNumber - 3; j++)
            {
                if (detectors[i][j].P2Token && detectors[i - 1][j + 1].P2Token && detectors[i - 2][j + 2].P2Token && detectors[i - 3][j + 3].P2Token)
                {
                    return true;
                }
            }
        }

        // descendingDiagonalCheck
        for (int i = 3; i < maxTopNumber; i++)
        {
            for (int j = 3; j < maxRightNumber; j++)
            {
                if (detectors[i][j].P2Token && detectors[i - 1][j - 1].P2Token && detectors[i - 2][j - 2].P2Token && detectors[i - 3][j - 3].P2Token)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
